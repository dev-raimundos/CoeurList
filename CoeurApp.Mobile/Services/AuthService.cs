using System.Net.Http.Json;
using System.Text.Json;
using System.Web;
using Coeur.Mobile.Application.Authentication;
using Coeur.Mobile.Application.Http;
using CoeurList.Config;
using Microsoft.Maui.Authentication;

namespace CoeurList.Services;

public class AuthService
{
    private const string SessionStorageKey = "coeur_auth_session";

    private readonly ICoeurApiClient _apiClient;
    private readonly TokenAccessor _tokenAccessor;
    private readonly Task _initialization;

    public AuthService(ICoeurApiClient apiClient, TokenAccessor tokenAccessor)
    {
        _apiClient = apiClient;
        _tokenAccessor = tokenAccessor;
        _initialization = LoadSessionAsync();
    }

    public AuthSession? CurrentSession { get; private set; }

    public bool IsAuthenticated => CurrentSession is not null;

    public event Action? OnChange;

    public Task EnsureInitializedAsync() => _initialization;

    public async Task<bool> LoginWithGoogleAsync()
    {
        var idToken = await AuthenticateWithGoogleAsync();
        if (idToken is null)
        {
            return false;
        }

        var auth = await _apiClient.LoginWithGoogleAsync(idToken);

        CurrentSession = new AuthSession(auth.User.Id, auth.User.Name, auth.User.Email, auth.Token);
        _tokenAccessor.Token = auth.Token;
        await SecureStorage.Default.SetAsync(SessionStorageKey, JsonSerializer.Serialize(CurrentSession));
        OnChange?.Invoke();
        return true;
    }

    public Task LogoutAsync()
    {
        CurrentSession = null;
        _tokenAccessor.Token = null;
        SecureStorage.Default.Remove(SessionStorageKey);
        OnChange?.Invoke();
        return Task.CompletedTask;
    }

    private static async Task<string?> AuthenticateWithGoogleAsync()
    {
        var codeVerifier = PkceHelper.GenerateCodeVerifier();
        var codeChallenge = PkceHelper.GenerateCodeChallenge(codeVerifier);

        var query = HttpUtility.ParseQueryString(string.Empty);
        query["client_id"] = AppConfig.GoogleClientId;
        query["redirect_uri"] = AppConfig.OAuthRedirectUrl;
        query["response_type"] = "code";
        query["scope"] = "openid email profile";
        query["code_challenge"] = codeChallenge;
        query["code_challenge_method"] = "S256";

        var authorizeUri = new Uri($"{AppConfig.GoogleAuthorizationEndpoint}?{query}");
        var callbackUri = new Uri(AppConfig.OAuthRedirectUrl);

        var authResult = await WebAuthenticator.Default.AuthenticateAsync(authorizeUri, callbackUri);

        if (!authResult.Properties.TryGetValue("code", out var code) || string.IsNullOrEmpty(code))
        {
            return null;
        }

        using var httpClient = new HttpClient();
        var tokenResponse = await httpClient.PostAsync(AppConfig.GoogleTokenEndpoint, new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["client_id"] = AppConfig.GoogleClientId,
            ["code"] = code,
            ["code_verifier"] = codeVerifier,
            ["grant_type"] = "authorization_code",
            ["redirect_uri"] = AppConfig.OAuthRedirectUrl,
        }));

        tokenResponse.EnsureSuccessStatusCode();

        var tokenPayload = await tokenResponse.Content.ReadFromJsonAsync<GoogleTokenResponse>();
        return tokenPayload?.IdToken;
    }

    private async Task LoadSessionAsync()
    {
        try
        {
            var json = await SecureStorage.Default.GetAsync(SessionStorageKey);
            if (!string.IsNullOrEmpty(json))
            {
                CurrentSession = JsonSerializer.Deserialize<AuthSession>(json);
                _tokenAccessor.Token = CurrentSession?.Token;
            }
        }
        catch
        {
            CurrentSession = null;
        }
    }

    private sealed record GoogleTokenResponse(
        [property: System.Text.Json.Serialization.JsonPropertyName("id_token")] string IdToken
    );
}

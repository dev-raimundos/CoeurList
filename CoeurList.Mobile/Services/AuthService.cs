using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Coeur.Mobile.Application.Authentication;

namespace CoeurList.Services;

public class AuthService
{
    private const string SessionStorageKey = "coeur_auth_session";
    private const string AuthorizeEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";
    private const string TokenEndpoint = "https://oauth2.googleapis.com/token";

    private static readonly HttpClient Http = new();

    private readonly IGoogleAuthBroker _authBroker;
    private readonly Task _initialization;

    public AuthService(IGoogleAuthBroker authBroker)
    {
        _authBroker = authBroker;
        _initialization = LoadSessionAsync();
    }

    public AuthSession? CurrentSession { get; private set; }

    public bool IsAuthenticated => CurrentSession is { } session && session.ExpiresAtUtc > DateTimeOffset.UtcNow;

    public event Action? OnChange;

    public Task EnsureInitializedAsync() => _initialization;

    public async Task<bool> LoginWithGoogleAsync()
    {
        var codeVerifier = GenerateCodeVerifier();
        var codeChallenge = GenerateCodeChallenge(codeVerifier);
        var state = Guid.NewGuid().ToString("N");

        var authorizeUri = BuildAuthorizeUri(codeChallenge, state);

        IReadOnlyDictionary<string, string> callbackParams;
        try
        {
            callbackParams = await _authBroker.AuthenticateAsync(authorizeUri);
        }
        catch (TaskCanceledException)
        {
            return false;
        }

        if (!callbackParams.TryGetValue("state", out var returnedState) || returnedState != state)
        {
            return false;
        }

        if (!callbackParams.TryGetValue("code", out var code))
        {
            return false;
        }

        var session = await ExchangeCodeAsync(code, codeVerifier);
        if (session is null)
        {
            return false;
        }

        CurrentSession = session;
        await SecureStorage.Default.SetAsync(SessionStorageKey, JsonSerializer.Serialize(session));
        OnChange?.Invoke();
        return true;
    }

    public Task LogoutAsync()
    {
        CurrentSession = null;
        SecureStorage.Default.Remove(SessionStorageKey);
        OnChange?.Invoke();
        return Task.CompletedTask;
    }

    private async Task LoadSessionAsync()
    {
        try
        {
            var json = await SecureStorage.Default.GetAsync(SessionStorageKey);
            if (!string.IsNullOrEmpty(json))
            {
                CurrentSession = JsonSerializer.Deserialize<AuthSession>(json);
            }
        }
        catch
        {
            CurrentSession = null;
        }
    }

    private Uri BuildAuthorizeUri(string codeChallenge, string state)
    {
        var query = new Dictionary<string, string>
        {
            ["client_id"] = _authBroker.ClientId,
            ["redirect_uri"] = _authBroker.RedirectUri.ToString(),
            ["response_type"] = "code",
            ["scope"] = GoogleAuthOptions.Scope,
            ["code_challenge"] = codeChallenge,
            ["code_challenge_method"] = "S256",
            ["state"] = state,
            ["prompt"] = "select_account",
        };

        var queryString = string.Join("&", query.Select(kv => $"{kv.Key}={Uri.EscapeDataString(kv.Value)}"));
        return new Uri($"{AuthorizeEndpoint}?{queryString}");
    }

    private async Task<AuthSession?> ExchangeCodeAsync(string code, string codeVerifier)
    {
        var request = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["client_id"] = _authBroker.ClientId,
            ["redirect_uri"] = _authBroker.RedirectUri.ToString(),
            ["grant_type"] = "authorization_code",
            ["code"] = code,
            ["code_verifier"] = codeVerifier,
        });

        using var response = await Http.PostAsync(TokenEndpoint, request);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        using var stream = await response.Content.ReadAsStreamAsync();
        var payload = await JsonSerializer.DeserializeAsync<GoogleTokenResponse>(stream);
        if (payload?.IdToken is null)
        {
            return null;
        }

        var claims = DecodeIdTokenClaims(payload.IdToken);

        return new AuthSession(
            UserId: claims.GetValueOrDefault("sub", string.Empty),
            Email: claims.GetValueOrDefault("email", string.Empty),
            Name: claims.GetValueOrDefault("name", string.Empty),
            PictureUrl: claims.GetValueOrDefault("picture", string.Empty),
            IdToken: payload.IdToken,
            AccessToken: payload.AccessToken,
            ExpiresAtUtc: DateTimeOffset.UtcNow.AddSeconds(payload.ExpiresIn));
    }

    private static Dictionary<string, string> DecodeIdTokenClaims(string idToken)
    {
        var parts = idToken.Split('.');
        if (parts.Length < 2)
        {
            return new Dictionary<string, string>();
        }

        var payloadJson = Encoding.UTF8.GetString(Base64UrlDecode(parts[1]));
        using var document = JsonDocument.Parse(payloadJson);

        return document.RootElement.EnumerateObject()
            .ToDictionary(p => p.Name, p => p.Value.ToString());
    }

    private static byte[] Base64UrlDecode(string input)
    {
        var base64 = input.Replace('-', '+').Replace('_', '/');
        base64 += (base64.Length % 4) switch
        {
            2 => "==",
            3 => "=",
            _ => string.Empty,
        };
        return Convert.FromBase64String(base64);
    }

    private static string GenerateCodeVerifier() => Base64UrlEncode(RandomNumberGenerator.GetBytes(32));

    private static string GenerateCodeChallenge(string codeVerifier) =>
        Base64UrlEncode(SHA256.HashData(Encoding.ASCII.GetBytes(codeVerifier)));

    private static string Base64UrlEncode(byte[] bytes) =>
        Convert.ToBase64String(bytes).TrimEnd('=').Replace('+', '-').Replace('/', '_');

    private sealed class GoogleTokenResponse
    {
        [JsonPropertyName("id_token")]
        public string? IdToken { get; set; }

        [JsonPropertyName("access_token")]
        public string? AccessToken { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
    }
}

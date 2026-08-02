using System.Text.Json;
using CoeurMobile.App.Core.Http;

namespace CoeurMobile.App.Modules.Auth.Services;

public class AuthService
{
    private readonly ICoeurApiClient _apiClient;
    private readonly TokenAccessor _tokenAccessor;
    private readonly ISecureSessionStore _sessionStore;
    private readonly Task _initialization;

    public AuthService(ICoeurApiClient apiClient, TokenAccessor tokenAccessor, ISecureSessionStore sessionStore)
    {
        _apiClient = apiClient;
        _tokenAccessor = tokenAccessor;
        _sessionStore = sessionStore;
        _initialization = LoadSessionAsync();
    }

    public AuthSession? CurrentSession { get; private set; }

    public bool IsAuthenticated => CurrentSession is not null;

    public event Action? OnChange;

    public Task EnsureInitializedAsync() => _initialization;

    public async Task LoginAsync(string email, string password)
    {
        var auth = await _apiClient.LoginAsync(email, password);

        CurrentSession = new AuthSession(auth.User.Id, auth.User.Name, auth.User.Email, auth.Token);
        _tokenAccessor.Token = auth.Token;
        await _sessionStore.SetAsync(JsonSerializer.Serialize(CurrentSession));
        OnChange?.Invoke();
    }

    public Task LogoutAsync()
    {
        CurrentSession = null;
        _tokenAccessor.Token = null;
        _sessionStore.Remove();
        OnChange?.Invoke();
        return Task.CompletedTask;
    }

    private async Task LoadSessionAsync()
    {
        try
        {
            var json = await _sessionStore.GetAsync();
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
}

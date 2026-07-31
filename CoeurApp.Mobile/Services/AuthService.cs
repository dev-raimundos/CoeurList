using System.Text.Json;
using Coeur.Mobile.Application.Authentication;

namespace CoeurList.Services;

public class AuthService
{
    private const string SessionStorageKey = "coeur_auth_session";

    private readonly Task _initialization;

    public AuthService()
    {
        _initialization = LoadSessionAsync();
    }

    public AuthSession? CurrentSession { get; private set; }

    public bool IsAuthenticated => CurrentSession is not null;

    public event Action? OnChange;

    public Task EnsureInitializedAsync() => _initialization;

    /// <summary>
    /// TODO: integrar com o endpoint de autenticação do CoeurApi e validar a senha lá.
    /// Por enquanto só exige que os dois campos estejam preenchidos.
    /// </summary>
    public async Task<bool> LoginAsync(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            return false;
        }

        CurrentSession = new AuthSession(email);
        await SecureStorage.Default.SetAsync(SessionStorageKey, JsonSerializer.Serialize(CurrentSession));
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
}

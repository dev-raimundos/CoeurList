namespace CoeurMobile.App.Modules.Auth.Platform;

public class MauiSecureSessionStore
{
    private const string SessionStorageKey = "coeur_auth_session";

    public Task<string?> GetAsync()
    {
        return SecureStorage.Default.GetAsync(SessionStorageKey);
    }

    public Task SetAsync(string value)
    {
        return SecureStorage.Default.SetAsync(SessionStorageKey, value);
    }

    public void Remove()
    {
        SecureStorage.Default.Remove(SessionStorageKey);
    }
}

using CoeurMobile.App.Core.Services;

namespace CoeurMobile.App.Core.Platform;

public class MauiSecureSessionStore : ISecureSessionStore
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

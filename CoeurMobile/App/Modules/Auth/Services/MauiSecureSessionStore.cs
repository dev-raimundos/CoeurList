namespace CoeurMobile.App.Modules.Auth.Services;

public class MauiSecureSessionStore : ISecureSessionStore
{
    private const string SessionStorageKey = "coeur_auth_session";

    public Task<string?> GetAsync() => SecureStorage.Default.GetAsync(SessionStorageKey);

    public Task SetAsync(string value) => SecureStorage.Default.SetAsync(SessionStorageKey, value);

    public void Remove() => SecureStorage.Default.Remove(SessionStorageKey);
}

namespace CoeurMobile.App.Modules.Auth.Services;

public interface ISecureSessionStore
{
    Task<string?> GetAsync();

    Task SetAsync(string value);

    void Remove();
}

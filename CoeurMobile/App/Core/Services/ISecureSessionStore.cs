namespace CoeurMobile.App.Core.Services;

public interface ISecureSessionStore
{
    Task<string?> GetAsync();

    Task SetAsync(string value);

    void Remove();
}

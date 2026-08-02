using CoeurMobile.App.Modules.Auth.Dtos;

namespace CoeurMobile.App.Core.Http;

public interface ICoeurApiClient
{
    Task<AuthResponse> LoginAsync(string email, string password, CancellationToken cancellationToken = default);
}

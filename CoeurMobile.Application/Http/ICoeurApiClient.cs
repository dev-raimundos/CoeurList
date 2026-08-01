using CoeurMobile.Application.DTOs.Responses;

namespace CoeurMobile.Application.Http;

public interface ICoeurApiClient
{
    Task<AuthResponse> LoginWithGoogleAsync(string idToken, CancellationToken cancellationToken = default);
}

using Coeur.Mobile.Application.DTOs.Responses;

namespace Coeur.Mobile.Application.Http;

public interface ICoeurApiClient
{
    Task<AuthResponse> LoginWithGoogleAsync(string idToken, CancellationToken cancellationToken = default);
}

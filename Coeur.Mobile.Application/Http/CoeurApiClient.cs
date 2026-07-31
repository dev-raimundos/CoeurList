using System.Net.Http.Json;
using Coeur.Mobile.Application.DTOs.Requests;
using Coeur.Mobile.Application.DTOs.Responses;

namespace Coeur.Mobile.Application.Http;

public class CoeurApiClient(HttpClient httpClient) : ICoeurApiClient
{
    public async Task<AuthResponse> LoginWithGoogleAsync(string idToken, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("api/v1/auth/google", new GoogleLoginRequest(idToken), cancellationToken);
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadFromJsonAsync<AuthResponse>(cancellationToken);
        return body ?? throw new InvalidOperationException("Resposta vazia do endpoint de login com Google.");
    }
}

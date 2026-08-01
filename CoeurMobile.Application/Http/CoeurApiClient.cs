using System.Net.Http.Json;
using CoeurMobile.Application.DTOs.Requests;
using CoeurMobile.Application.DTOs.Responses;

namespace CoeurMobile.Application.Http;

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

using System.Net.Http.Json;
using System.Text.Json.Serialization;
using CoeurMobile.App.Modules.Auth.Dtos;

namespace CoeurMobile.App.Core.Http;

public class CoeurApiClient(HttpClient httpClient) : ICoeurApiClient
{
    public async Task<AuthResponse> LoginAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("api/v1/auth/login", new LoginRequest(email, password), cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var problem = await response.Content.ReadFromJsonAsync<ProblemDetailsResponse>(cancellationToken);
            throw new CoeurApiException(problem?.Detail ?? "Não foi possível entrar. Tente novamente.");
        }

        var body = await response.Content.ReadFromJsonAsync<AuthResponse>(cancellationToken);
        return body ?? throw new CoeurApiException("Resposta vazia do endpoint de login.");
    }

    private sealed record ProblemDetailsResponse([property: JsonPropertyName("detail")] string? Detail);
}

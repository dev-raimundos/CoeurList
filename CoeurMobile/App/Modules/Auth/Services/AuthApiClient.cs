using System.Net.Http.Json;
using CoeurMobile.App.Core.Http.Client;
using CoeurMobile.App.Modules.Auth.Dtos;

namespace CoeurMobile.App.Modules.Auth.Services;

public class AuthApiClient(HttpClient httpClient)
{
    public async Task<AuthResponse> LoginAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync(
                "api/v1/auth/login",
                new LoginRequest(email, password),
                cancellationToken
            );

        var body = await response.Content.ReadFromJsonAsync<AuthResponse>(cancellationToken);

        return body ?? throw new CoeurApiException("Resposta vazia do endpoint de login.");
    }

    public async Task<MeResponse> GetMeAsync(CancellationToken cancellationToken = default)
    {
        var me = await httpClient.GetFromJsonAsync<MeResponse>("api/v1/auth/me", cancellationToken);

        return me ?? throw new CoeurApiException("Resposta vazia do endpoint /me.");
    }
}

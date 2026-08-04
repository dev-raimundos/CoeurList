using System.Net.Http.Json;
using CoeurMobile.App.Core.Http.Client.Dtos;

namespace CoeurMobile.App.Core.Http.Client;

/// <summary>
/// Implementação concreta de <see cref="ICoeurApiClient"/>. Recebe um <see cref="HttpClient"/> já configurado
/// (URL base + os <c>DelegatingHandler</c>s de <c>Handlers/</c>) via injeção de dependência — o construtor
/// primário <c>(HttpClient httpClient)</c> é um jeito mais curto do C# de declarar campo e construtor de uma vez só.
/// </summary>
public class CoeurApiClient(HttpClient httpClient) : ICoeurApiClient
{
    /// <inheritdoc/>
    public async Task<AuthResponse> LoginAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        // PostAsJsonAsync serializa o objeto em JSON e faz o POST, tudo em uma chamada só.
        var response = await httpClient.PostAsJsonAsync(
                "api/v1/auth/login",
                new LoginRequest(email, password),
                cancellationToken
            );

        // ReadFromJsonAsync faz o inverso: desserializa o corpo da resposta JSON de volta pra um objeto C#.
        var body = await response.Content.ReadFromJsonAsync<AuthResponse>(cancellationToken);

        // Se a resposta não foi bem-sucedida, quem já lançou a exceção foi o ApiExceptionHandler, lá no
        // pipeline do HttpClient, antes mesmo desta linha rodar — então esse "?? throw" só cobre o caso raro
        // de a API devolver 200 OK com corpo vazio.
        return body ?? throw new CoeurApiException("Resposta vazia do endpoint de login.");
    }

    /// <inheritdoc/>
    public async Task<MeResponse> GetMeAsync(CancellationToken cancellationToken = default)
    {
        var me = await httpClient.GetFromJsonAsync<MeResponse>("api/v1/auth/me", cancellationToken);

        return me ?? throw new CoeurApiException("Resposta vazia do endpoint /me.");
    }
}

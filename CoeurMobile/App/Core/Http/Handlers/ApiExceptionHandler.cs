using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using CoeurMobile.App.Core.Http.Client;
using CoeurMobile.App.Core.Services;

namespace CoeurMobile.App.Core.Http.Handlers;

/// <summary>
/// Segundo <see cref="DelegatingHandler"/> do pipeline (roda depois do <see cref="BearerTokenHandler"/>, ver
/// a ordem em <c>MauiProgram.cs</c>). É o equivalente ao interceptor de erros do Angular: centraliza o
/// tratamento de qualquer chamada HTTP que falhe, disparando um toast — assim nenhuma tela/componente
/// precisa saber ler <c>ProblemDetails</c> nem decidir qual mensagem mostrar. Também é quem detecta um token
/// morto (<c>401</c>) e avisa o <see cref="TokenAccessor"/>, que por sua vez aciona o logout automático no
/// <c>AuthService</c> — fechando o "guard" de navegação pra sessões com token expirado.
/// </summary>
public class ApiExceptionHandler(IToastService toastService, TokenAccessor tokenAccessor) : DelegatingHandler
{
    /// <summary>
    /// Deixa o request seguir (<c>base.SendAsync</c>) e depois confere o resultado: se a rede falhar
    /// (sem internet, servidor fora do ar) captura o <see cref="HttpRequestException"/>; se a resposta não
    /// for 2xx, lê o corpo como <c>ProblemDetails</c> (o formato padrão de erro da Coeur API) e usa o campo
    /// <c>toast</c> que a própria API já manda pronto. Em ambos os casos, mostra o toast e relança como
    /// <see cref="CoeurApiException"/>, pra quem chamou (ex.: <c>Login.razor.cs</c>) só precisar de um
    /// <c>catch</c> genérico pra parar o loading, sem exibir nada.
    /// </summary>
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        HttpResponseMessage response;

        try
        {
            response = await base.SendAsync(request, cancellationToken);
        }
        catch (HttpRequestException)
        {
            toastService.Show("Não foi possível conectar ao servidor. Verifique sua conexão.");
            throw;
        }

        if (response.IsSuccessStatusCode)
        {
            return response;
        }

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            // Token salvo não é mais válido (expirou ou foi revogado) — força o logout, que por sua vez
            // aciona o guard de navegação (AuthorizeRouteView) a redirecionar pro login.
            tokenAccessor.NotifyUnauthorized();
        }

        ProblemDetailsPayload? problem = null;
        try
        {
            problem = await response.Content.ReadFromJsonAsync<ProblemDetailsPayload>(cancellationToken: cancellationToken);
        }
        catch (Exception)
        {
            // Corpo não era o ProblemDetails esperado (ex.: uma página de erro HTML de algo na frente da API,
            // fora do nosso controle) — cai pro fallback genérico abaixo em vez de propagar um JsonException.
        }

        var message = problem?.Toast?.Message ?? problem?.Detail ?? "Ocorreu um erro inesperado.";

        var severity = problem?.Toast?.Type switch
        {
            "warning" => ToastSeverity.Warning,
            "info" => ToastSeverity.Info,
            _ => ToastSeverity.Error,
        };

        toastService.Show(message, severity);
        throw new CoeurApiException(message);
    }

    /// <summary>Só o pedacinho do <c>toast</c> que a API embute no Problem Details (<c>{ type, message }</c>).</summary>
    private sealed record ToastPayload(
        [property: JsonPropertyName("type")] string? Type,
        [property: JsonPropertyName("message")] string? Message);

    /// <summary>
    /// Formato de erro que a Coeur API sempre devolve (RFC 9457 Problem Details). Um <c>record</c> é um tipo
    /// do C# pensado pra dados imutáveis — aqui só serve pra descrever o formato do JSON que chega, então
    /// nem precisa de uma classe "de verdade" com lógica.
    /// </summary>
    private sealed record ProblemDetailsPayload(
        [property: JsonPropertyName("detail")] string? Detail,
        [property: JsonPropertyName("toast")] ToastPayload? Toast);
}

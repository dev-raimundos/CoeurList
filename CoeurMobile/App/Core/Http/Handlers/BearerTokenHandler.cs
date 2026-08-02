using System.Net.Http.Headers;

namespace CoeurMobile.App.Core.Http.Handlers;

/// <summary>
/// Um <see cref="DelegatingHandler"/> é o "interceptor" de HTTP do .NET — o mesmo conceito de um
/// <c>HttpInterceptor</c> do Angular. Ele fica no meio do caminho entre a chamada que seu código faz
/// (ex.: <c>httpClient.PostAsJsonAsync(...)</c>) e o envio de fato pela rede, podendo inspecionar ou alterar
/// tanto o request quanto o response. Vários handlers podem ser encadados (ver
/// <c>.AddHttpMessageHandler&lt;T&gt;()</c> em <c>MauiProgram.cs</c>), formando um pipeline — igual uma
/// cadeia de middlewares.
/// <para>
/// Este handler específico só faz uma coisa: antes de deixar o request seguir, anexa o header
/// <c>Authorization: Bearer &lt;token&gt;</c> usando o valor guardado em <see cref="TokenAccessor"/>.
/// </para>
/// </summary>
public class BearerTokenHandler(TokenAccessor tokenAccessor) : DelegatingHandler
{
    /// <summary>
    /// Sobrescreve (<see langword="override"/>) o método que o pipeline chama pra cada request.
    /// <c>base.SendAsync(...)</c> é o que repassa o request adiante — pro próximo handler da cadeia
    /// (aqui, o <see cref="ApiExceptionHandler"/>).
    /// </summary>
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = tokenAccessor.Token;
        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return base.SendAsync(request, cancellationToken);
    }
}

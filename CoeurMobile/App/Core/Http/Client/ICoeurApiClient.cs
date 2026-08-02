using CoeurMobile.App.Modules.Auth.Dtos;

namespace CoeurMobile.App.Core.Http.Client;

/// <summary>
/// Contrato (interface) do cliente HTTP da Coeur API. Uma <c>interface</c> em C# só declara a assinatura
/// dos métodos — quem faz o trabalho de verdade é quem a implementa (<see cref="CoeurApiClient"/>).
/// Depender da interface (em vez da classe concreta) é o que permite trocar a implementação — por exemplo,
/// um "dublê" nos testes — sem mudar quem consome esse serviço, como o <c>AuthService</c>.
/// </summary>
public interface ICoeurApiClient
{
    /// <summary>Chama o endpoint de login da API e devolve o usuário autenticado + o token JWT.</summary>
    /// <param name="email">Email informado no formulário de login.</param>
    /// <param name="password">Senha em texto puro — só é seguro porque a chamada roda sobre HTTPS; a API nunca a guarda, só compara o hash.</param>
    /// <param name="cancellationToken">
    /// Token de cancelamento padrão do .NET: permite abortar a chamada (ex.: o usuário saiu da tela) sem
    /// esperar a resposta do servidor. Tem valor padrão (<c>default</c>) pra quem não precisa se preocupar com isso.
    /// </param>
    /// <returns>
    /// Uma <see cref="Task{TResult}"/> — o jeito do .NET de representar "um valor que ainda vai chegar"
    /// (chamada assíncrona) — contendo os dados do usuário e o token.
    /// </returns>
    Task<AuthResponse> LoginAsync(string email, string password, CancellationToken cancellationToken = default);
}

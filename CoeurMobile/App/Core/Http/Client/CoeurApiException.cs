namespace CoeurMobile.App.Core.Http.Client;

/// <summary>
/// Exceção própria da Coeur API, lançada pelo <see cref="Handlers.ApiExceptionHandler"/> sempre que uma
/// chamada HTTP falha (rede indisponível ou resposta de erro da API). Ter um tipo próprio — em vez de deixar
/// vazar um <see cref="HttpRequestException"/> ou outro erro genérico do .NET — permite que quem chama a API
/// (ex.: a página de Login) diferencie "erro esperado, já tratado pela API" de um bug inesperado qualquer,
/// caso um dia precise reagir de forma diferente a cada um.
/// </summary>
/// <remarks>
/// <c>sealed</c> significa que esta classe não pode ser herdada — não faz sentido existir uma "subclasse"
/// de uma exceção tão simples, então o compilador já barra isso de propósito.
/// A sintaxe <c>(string message) : Exception(message)</c> é um construtor primário: recebe a mensagem e já
/// repassa direto pro construtor da classe base (<see cref="Exception"/>), sem precisar escrever um corpo.
/// </remarks>
public sealed class CoeurApiException(string message) : Exception(message);

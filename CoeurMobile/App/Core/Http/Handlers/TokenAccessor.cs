namespace CoeurMobile.App.Core.Http.Handlers;

/// <summary>
/// Guarda o token JWT atual em memória — sem nenhuma lógica, só uma propriedade pública com <c>get</c>/<c>set</c>.
/// Existe pra resolver uma dependência: o <see cref="BearerTokenHandler"/> (que roda a cada chamada HTTP)
/// precisa saber o token atual, mas quem sabe fazer login é o <c>AuthService</c> — e o <c>AuthService</c> não
/// deveria conhecer detalhes de HTTP. Registrando esta classe como <c>Singleton</c> na injeção de dependência
/// (ver <c>MauiProgram.cs</c>), as duas pontas enxergam a mesma instância: o <c>AuthService</c> escreve o
/// token aqui depois do login, e o <see cref="BearerTokenHandler"/> só lê.
/// </summary>
public class TokenAccessor
{
    /// <summary>Token JWT atual, ou <see langword="null"/> quando não há usuário autenticado.</summary>
    public string? Token { get; set; }
}

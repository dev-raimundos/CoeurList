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

    /// <summary>
    /// Disparado pelo <see cref="ApiExceptionHandler"/> quando alguma chamada volta com <c>401 Unauthorized</c>
    /// — sinal de que o token salvo não é mais válido (expirou, foi revogado, etc.). O <c>AuthService</c> se
    /// inscreve nesse evento pra se auto-deslogar quando isso acontece, mesmo sem ninguém ter clicado em
    /// "sair". Mesmo esquema de pub/sub do <see cref="Services.IToastService"/>, só que na direção contrária
    /// (dos <c>Handlers</c> pro <c>AuthService</c>, em vez do <c>AuthService</c> pros <c>Handlers</c> via <see cref="Token"/>).
    /// </summary>
    public event Action? OnUnauthorized;

    /// <summary>Notifica os inscritos de que o token atual foi rejeitado pela API.</summary>
    public void NotifyUnauthorized()
    {
        OnUnauthorized?.Invoke();
    }
}

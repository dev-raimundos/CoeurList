namespace CoeurList.Services;

/// <summary>
/// Abstrai a parte do fluxo OAuth que depende de plataforma: abrir o navegador do sistema
/// e capturar o redirect com o authorization code. A troca do code por tokens é feita
/// pelo AuthService, que é igual em todas as plataformas.
/// </summary>
public interface IGoogleAuthBroker
{
    string ClientId { get; }

    Uri RedirectUri { get; }

    Task<IReadOnlyDictionary<string, string>> AuthenticateAsync(Uri authorizeUri);
}

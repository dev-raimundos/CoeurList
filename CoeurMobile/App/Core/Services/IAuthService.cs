namespace CoeurMobile.App.Core.Services;

/// <summary>
/// Contrato mínimo de sessão que qualquer módulo pode consumir (igual o <see cref="ToastService"/>) — só o
/// necessário pra saber "quem está logado" e deslogar. <c>LoginAsync</c> fica de fora de propósito: só a
/// tela de login (dentro do próprio módulo Auth) precisa dele, então ela injeta o <c>AuthService</c>
/// concreto diretamente em vez de passar por essa interface. Diferente do <see cref="ToastService"/>, essa
/// interface existe mesmo — não pra "testabilidade" genérica, mas porque <c>Core</c> não pode depender do
/// <c>AuthService</c> concreto (que mora em <c>Modules/Auth</c>) sem recriar o acoplamento Core→Módulo que
/// motivou essa separação.
/// </summary>
public interface IAuthService
{
    AuthSession? CurrentSession { get; }

    bool IsAuthenticated();

    event Action? OnChange;

    Task EnsureInitializedAsync();

    Task LogoutAsync();
}

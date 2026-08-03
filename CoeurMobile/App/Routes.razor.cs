using CoeurMobile.App.Modules.Auth.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace CoeurMobile.App;

/// <summary>
/// Guarda de navegação central — equivalente a um <c>canActivate</c>/interceptor de rota do Angular.
/// Existe porque depender só de <c>[Authorize]</c>/<c>[AllowAnonymous]</c> nas páginas (via
/// <c>AuthorizeRouteView</c> + <c>FallbackPolicy</c>, configurados em <c>MauiProgram.cs</c>) não bloqueava
/// de forma confiável páginas sem o atributo explícito, como a Home — o usuário caía direto nela sem sessão.
/// </summary>
public partial class Routes : IDisposable
{
    private const string LoginRoute = "login";

    [Inject]
    protected AuthService AuthService { get; set; } = default!;

    [Inject]
    protected NavigationManager NavigationManager { get; set; } = default!;

    protected override void OnInitialized()
    {
        // Cobre o caso da sessão morrer com o usuário já numa tela protegida (ex.: token rejeitado no meio
        // do uso) — sem uma navegação nova, só o OnNavigateAsync abaixo não seria suficiente.
        AuthService.OnChange += HandleAuthChanged;
    }

    public void Dispose()
    {
        AuthService.OnChange -= HandleAuthChanged;
    }

    /// <summary>Roda antes de QUALQUER navegação renderizar, inclusive a primeira ao abrir o app.</summary>
    private async Task OnNavigateAsync(NavigationContext context)
    {
        await AuthService.EnsureInitializedAsync();
        RedirectToLoginIfNeeded(context.Path);
    }

    private void HandleAuthChanged()
    {
        RedirectToLoginIfNeeded(NavigationManager.ToBaseRelativePath(NavigationManager.Uri));
    }

    private void RedirectToLoginIfNeeded(string path)
    {
        var isLoginRoute = path.Trim('/').Equals(LoginRoute, StringComparison.OrdinalIgnoreCase);
        if (!AuthService.IsAuthenticated() && !isLoginRoute)
        {
            NavigationManager.NavigateTo($"/{LoginRoute}", replace: true);
        }
    }
}

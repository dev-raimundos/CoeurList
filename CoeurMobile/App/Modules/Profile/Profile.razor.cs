using CoeurMobile.App.Core.Services;
using CoeurMobile.App.Modules.Auth.Services;
using Microsoft.AspNetCore.Components;

namespace CoeurMobile.App.Modules.Profile;

public partial class Profile
{
    private const string BuildConfiguration =
#if DEBUG
        "Debug";
#else
        "Release";
#endif

    [Inject]
    protected ThemeService ThemeService { get; set; } = default!;

    [Inject]
    protected AuthService AuthService { get; set; } = default!;

    [Inject]
    protected NavigationManager NavigationManager { get; set; } = default!;

    private async Task LogoutAsync()
    {
        await AuthService.LogoutAsync();
        NavigationManager.NavigateTo("/login", replace: true);
    }
}

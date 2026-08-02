using CoeurMobile.App.Core.Services;
using Microsoft.AspNetCore.Components;

namespace CoeurMobile.App.Core.Layout.MainLayout;

public partial class MainLayout : IDisposable
{
    [Inject]
    protected ThemeService ThemeService { get; set; } = default!;

    protected override void OnInitialized()
    {
        ThemeService.OnChange += StateHasChanged;
    }

    public void Dispose()
    {
        ThemeService.OnChange -= StateHasChanged;
    }
}

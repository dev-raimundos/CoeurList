using CoeurMobile.App.Core.Services;
using Microsoft.AspNetCore.Components;

namespace CoeurMobile.App.Modules.Palette;

public partial class Palette
{
    [Inject]
    protected ThemeService ThemeService { get; set; } = default!;

    private sealed record Swatch(string Name, string Bg, string Text);

    private sealed record Shade(string Name, string ColorVar);

    private sealed record ToneRow(string Label, Shade[] Shades);

    private static readonly Swatch[] MainSwatches =
    [
        new("Primary", "--mud-palette-primary", "--mud-palette-primary-text"),
        new("Secondary", "--mud-palette-secondary", "--mud-palette-secondary-text"),
        new("Tertiary", "--mud-palette-tertiary", "--mud-palette-tertiary-text"),
        new("Error", "--mud-palette-error", "--mud-palette-error-text"),
        new("Info", "--mud-palette-info", "--mud-palette-info-text"),
        new("Success", "--mud-palette-success", "--mud-palette-success-text"),
        new("Warning", "--mud-palette-warning", "--mud-palette-warning-text"),
    ];

    private static readonly Swatch[] SurfaceSwatches =
    [
        new("Background", "--mud-palette-background", "--mud-palette-text-primary"),
        new("Surface", "--mud-palette-surface", "--mud-palette-text-primary"),
        new("App Bar", "--mud-palette-appbar-background", "--mud-palette-appbar-text"),
        new("Drawer", "--mud-palette-drawer-background", "--mud-palette-drawer-text"),
    ];

    private static readonly ToneRow[] ToneRows =
    [
        new("Primary", [new("Lighten", "--mud-palette-primary-lighten"), new("Base", "--mud-palette-primary"), new("Darken", "--mud-palette-primary-darken")]),
        new("Secondary", [new("Lighten", "--mud-palette-secondary-lighten"), new("Base", "--mud-palette-secondary"), new("Darken", "--mud-palette-secondary-darken")]),
        new("Tertiary", [new("Lighten", "--mud-palette-tertiary-lighten"), new("Base", "--mud-palette-tertiary"), new("Darken", "--mud-palette-tertiary-darken")]),
        new("Error", [new("Lighten", "--mud-palette-error-lighten"), new("Base", "--mud-palette-error"), new("Darken", "--mud-palette-error-darken")]),
    ];
}

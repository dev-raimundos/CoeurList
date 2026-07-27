using MudBlazor;

namespace CoeurList.Theme;

public static class AppTheme
{
    public static MudTheme Theme => new()
    {
        PaletteLight = new PaletteLight
        {
            Primary = "#512BD4",
            Background = "#FFFFFF",
            AppbarBackground = "#512BD4",
        },
        PaletteDark = new PaletteDark
        {
            Primary = "#7B52F5",
            Background = "#1a1a1a",
            AppbarBackground = "#1a1a1a",
        }
    };
}

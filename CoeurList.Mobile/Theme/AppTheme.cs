using MudBlazor;

namespace CoeurList.Theme;

public static class AppTheme
{
    public static MudTheme Theme => new()
    {
        PaletteLight = new PaletteLight
        {
            // Marca / navegação
            Primary = "rgba(81,43,212,1)",
            PrimaryContrastText = "rgba(255,255,255,1)",
            Secondary = "rgba(255,64,129,1)",
            SecondaryContrastText = "rgba(255,255,255,1)",
            Tertiary = "rgba(30,200,165,1)",
            TertiaryContrastText = "rgba(255,255,255,1)",

            // Semânticas (feedback de estado)
            Info = "rgba(33,150,243,1)",
            InfoContrastText = "rgba(255,255,255,1)",
            Success = "rgba(0,200,83,1)",
            SuccessContrastText = "rgba(255,255,255,1)",
            Warning = "rgba(255,152,0,1)",
            WarningContrastText = "rgba(255,255,255,1)",
            Error = "rgba(244,67,54,1)",
            ErrorContrastText = "rgba(255,255,255,1)",

            // Base neutra
            Black = "rgba(39,44,52,1)",
            White = "rgba(255,255,255,1)",
            Dark = "rgba(66,66,66,1)",
            DarkContrastText = "rgba(255,255,255,1)",

            // Fundos e superfícies
            Background = "rgba(255,255,255,1)",
            BackgroundGray = "rgba(245,245,245,1)",
            Surface = "rgba(255,255,255,1)",
            AppbarBackground = "rgba(81,43,212,1)",
            AppbarText = "rgba(255,255,255,1)",
            DrawerBackground = "rgba(255,255,255,1)",
            DrawerText = "rgba(66,66,66,1)",
            DrawerIcon = "rgba(97,97,97,1)",

            // Texto e estados de ação
            TextPrimary = "rgba(66,66,66,1)",
            TextSecondary = "rgba(0,0,0,0.54)",
            TextDisabled = "rgba(0,0,0,0.38)",
            ActionDefault = "rgba(0,0,0,0.54)",
            ActionDisabled = "rgba(0,0,0,0.26)",
            ActionDisabledBackground = "rgba(0,0,0,0.12)",

            // Linhas, divisores e tabelas
            Divider = "rgba(224,224,224,1)",
            DividerLight = "rgba(0,0,0,0.8)",
            LinesDefault = "rgba(0,0,0,0.12)",
            LinesInputs = "rgba(189,189,189,1)",
            TableLines = "rgba(224,224,224,1)",
            TableStriped = "rgba(0,0,0,0.02)",
            TableHover = "rgba(0,0,0,0.04)",
            Skeleton = "rgba(0,0,0,0.11)",

            // Variações claras/escuras derivadas das cores principais
            PrimaryDarken = "rgb(62,44,221)",
            PrimaryLighten = "rgb(118,106,231)",
            SecondaryDarken = "rgb(255,31,105)",
            SecondaryLighten = "rgb(255,102,153)",
            TertiaryDarken = "rgb(25,169,140)",
            TertiaryLighten = "rgb(42,223,187)",
            InfoDarken = "rgb(12,128,223)",
            InfoLighten = "rgb(71,167,245)",
            SuccessDarken = "rgb(0,163,68)",
            SuccessLighten = "rgb(0,235,98)",
            WarningDarken = "rgb(214,129,0)",
            WarningLighten = "rgb(255,167,36)",
            ErrorDarken = "rgb(242,28,13)",
            ErrorLighten = "rgb(246,96,85)",
            DarkDarken = "rgb(46,46,46)",
            DarkLighten = "rgb(87,87,87)",

            // Tons de cinza utilitários
            GrayDefault = "#9E9E9E",
            GrayLight = "#BDBDBD",
            GrayLighter = "#E0E0E0",
            GrayDark = "#757575",
            GrayDarker = "#616161",

            // Overlays e opacidades (dialogs, hover, ripple)
            OverlayDark = "rgba(33,33,33,0.5)",
            OverlayLight = "rgba(255,255,255,0.5)",
            BorderOpacity = 1,
            HoverOpacity = 0.06,
            RippleOpacity = 0.1,
            RippleOpacitySecondary = 0.2,
        },
        PaletteDark = new PaletteDark
        {
            // Marca / navegação
            Primary = "rgba(123,82,245,1)",
            PrimaryContrastText = "rgba(255,255,255,1)",
            Secondary = "rgba(255,64,129,1)",
            SecondaryContrastText = "rgba(255,255,255,1)",
            Tertiary = "rgba(30,200,165,1)",
            TertiaryContrastText = "rgba(255,255,255,1)",

            // Semânticas (feedback de estado)
            Info = "rgba(50,153,255,1)",
            InfoContrastText = "rgba(255,255,255,1)",
            Success = "rgba(11,186,131,1)",
            SuccessContrastText = "rgba(255,255,255,1)",
            Warning = "rgba(255,168,0,1)",
            WarningContrastText = "rgba(255,255,255,1)",
            Error = "rgba(246,78,98,1)",
            ErrorContrastText = "rgba(255,255,255,1)",

            // Base neutra
            Black = "rgba(39,39,47,1)",
            White = "rgba(255,255,255,1)",
            Dark = "rgba(39,39,47,1)",
            DarkContrastText = "rgba(255,255,255,1)",

            // Fundos e superfícies
            Background = "rgba(26,26,26,1)",
            BackgroundGray = "rgba(39,39,47,1)",
            Surface = "rgba(55,55,64,1)",
            AppbarBackground = "rgba(26,26,26,1)",
            AppbarText = "rgba(255,255,255,0.7)",
            DrawerBackground = "rgba(39,39,47,1)",
            DrawerText = "rgba(255,255,255,0.5)",
            DrawerIcon = "rgba(255,255,255,0.5)",

            // Texto e estados de ação
            TextPrimary = "rgba(255,255,255,0.7)",
            TextSecondary = "rgba(255,255,255,0.5)",
            TextDisabled = "rgba(255,255,255,0.2)",
            ActionDefault = "rgba(173,173,177,1)",
            ActionDisabled = "rgba(255,255,255,0.26)",
            ActionDisabledBackground = "rgba(255,255,255,0.12)",

            // Linhas, divisores e tabelas
            Divider = "rgba(255,255,255,0.12)",
            DividerLight = "rgba(255,255,255,0.06)",
            LinesDefault = "rgba(255,255,255,0.12)",
            LinesInputs = "rgba(255,255,255,0.3)",
            TableLines = "rgba(255,255,255,0.12)",
            TableStriped = "rgba(255,255,255,0.2)",
            TableHover = "rgba(0,0,0,0.04)",
            Skeleton = "rgba(255,255,255,0.11)",

            // Variações claras/escuras derivadas das cores principais
            PrimaryDarken = "rgb(90,75,226)",
            PrimaryLighten = "rgb(151,141,236)",
            SecondaryDarken = "rgb(255,31,105)",
            SecondaryLighten = "rgb(255,102,153)",
            TertiaryDarken = "rgb(25,169,140)",
            TertiaryLighten = "rgb(42,223,187)",
            InfoDarken = "rgb(10,133,255)",
            InfoLighten = "rgb(92,173,255)",
            SuccessDarken = "rgb(9,154,108)",
            SuccessLighten = "rgb(13,222,156)",
            WarningDarken = "rgb(214,143,0)",
            WarningLighten = "rgb(255,182,36)",
            ErrorDarken = "rgb(244,47,70)",
            ErrorLighten = "rgb(248,119,134)",
            DarkDarken = "rgb(23,23,28)",
            DarkLighten = "rgb(56,56,67)",

            // Tons de cinza utilitários
            GrayDefault = "#9E9E9E",
            GrayLight = "#BDBDBD",
            GrayLighter = "#E0E0E0",
            GrayDark = "#757575",
            GrayDarker = "#616161",

            // Overlays e opacidades (dialogs, hover, ripple)
            OverlayDark = "rgba(33,33,33,0.5)",
            OverlayLight = "rgba(255,255,255,0.5)",
            BorderOpacity = 1,
            HoverOpacity = 0.06,
            RippleOpacity = 0.1,
            RippleOpacitySecondary = 0.2,
        }
    };
}

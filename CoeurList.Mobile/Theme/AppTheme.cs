using MudBlazor;

namespace CoeurList.Theme;

public static class AppTheme
{
    public static MudTheme Theme => new()
    {
        PaletteLight = new PaletteLight
        {
            // Primary
            Primary = "#512BD4",
            PrimaryContrastText = "#FFFFFF",
            PrimaryLighten = "#766AE7",
            PrimaryDarken = "#3E2CDD",

            // Secondary
            Secondary = "#FF4081",
            SecondaryContrastText = "#FFFFFF",
            SecondaryLighten = "#FF6699",
            SecondaryDarken = "#FF1F69",

            // Tertiary
            Tertiary = "#1EC8A5",
            TertiaryContrastText = "#FFFFFF",
            TertiaryLighten = "#2ADFBB",
            TertiaryDarken = "#19A98C",

            // Info
            Info = "#2196F3",
            InfoContrastText = "#FFFFFF",
            InfoLighten = "#47A7F5",
            InfoDarken = "#0C80DF",

            // Success
            Success = "#00C853",
            SuccessContrastText = "#FFFFFF",
            SuccessLighten = "#00EB62",
            SuccessDarken = "#00A344",

            // Warning
            Warning = "#FF9800",
            WarningContrastText = "#FFFFFF",
            WarningLighten = "#FFA724",
            WarningDarken = "#D68100",

            // Error
            Error = "#F44336",
            ErrorContrastText = "#FFFFFF",
            ErrorLighten = "#F66055",
            ErrorDarken = "#F21C0D",

            // Base neutra
            Black = "#272C34",
            White = "#FFFFFF",
            Dark = "#424242",
            DarkContrastText = "#FFFFFF",
            DarkLighten = "#575757",
            DarkDarken = "#2E2E2E",

            // Fundos e superfícies
            Background = "#FFFFFF",
            BackgroundGray = "#F5F5F5",
            Surface = "#FFFFFF",
            AppbarBackground = "#512BD4",
            AppbarText = "#FFFFFF",
            DrawerBackground = "#FFFFFF",
            DrawerText = "#424242",
            DrawerIcon = "#616161",

            // Texto e estados de ação
            TextPrimary = "#424242",
            TextSecondary = "#0000008A",
            TextDisabled = "#00000061",
            ActionDefault = "#0000008A",
            ActionDisabled = "#00000042",
            ActionDisabledBackground = "#0000001F",

            // Linhas, divisores e tabelas
            Divider = "#E0E0E0",
            DividerLight = "#000000CC",
            LinesDefault = "#0000001F",
            LinesInputs = "#BDBDBD",
            TableLines = "#E0E0E0",
            TableStriped = "#00000005",
            TableHover = "#0000000A",
            Skeleton = "#0000001C",

            // Tons de cinza utilitários
            GrayDefault = "#9E9E9E",
            GrayLight = "#BDBDBD",
            GrayLighter = "#E0E0E0",
            GrayDark = "#757575",
            GrayDarker = "#616161",

            // Overlays e opacidades (dialogs, hover, ripple)
            OverlayDark = "#21212180",
            OverlayLight = "#FFFFFF80",
            BorderOpacity = 1,
            HoverOpacity = 0.06,
            RippleOpacity = 0.1,
            RippleOpacitySecondary = 0.2,
        },
        PaletteDark = new PaletteDark
        {
            // Primary
            Primary = "#7B52F5",
            PrimaryContrastText = "#FFFFFF",
            PrimaryLighten = "#978DEC",
            PrimaryDarken = "#5A4BE2",

            // Secondary
            Secondary = "#FF4081",
            SecondaryContrastText = "#FFFFFF",
            SecondaryLighten = "#FF6699",
            SecondaryDarken = "#FF1F69",

            // Tertiary
            Tertiary = "#1EC8A5",
            TertiaryContrastText = "#FFFFFF",
            TertiaryLighten = "#2ADFBB",
            TertiaryDarken = "#19A98C",

            // Info
            Info = "#3299FF",
            InfoContrastText = "#FFFFFF",
            InfoLighten = "#5CADFF",
            InfoDarken = "#0A85FF",

            // Success
            Success = "#0BBA83",
            SuccessContrastText = "#FFFFFF",
            SuccessLighten = "#0DDE9C",
            SuccessDarken = "#099A6C",

            // Warning
            Warning = "#FFA800",
            WarningContrastText = "#FFFFFF",
            WarningLighten = "#FFB624",
            WarningDarken = "#D68F00",

            // Error
            Error = "#F64E62",
            ErrorContrastText = "#FFFFFF",
            ErrorLighten = "#F87786",
            ErrorDarken = "#F42F46",

            // Base neutra
            Black = "#27272F",
            White = "#FFFFFF",
            Dark = "#27272F",
            DarkContrastText = "#FFFFFF",
            DarkLighten = "#383843",
            DarkDarken = "#17171C",

            // Fundos e superfícies
            Background = "#1A1A1A",
            BackgroundGray = "#27272F",
            Surface = "#373740",
            AppbarBackground = "#1A1A1A",
            AppbarText = "#FFFFFFB3",
            DrawerBackground = "#27272F",
            DrawerText = "#FFFFFF80",
            DrawerIcon = "#FFFFFF80",

            // Texto e estados de ação
            TextPrimary = "#FFFFFFB3",
            TextSecondary = "#FFFFFF80",
            TextDisabled = "#FFFFFF33",
            ActionDefault = "#ADADB1",
            ActionDisabled = "#FFFFFF42",
            ActionDisabledBackground = "#FFFFFF1F",

            // Linhas, divisores e tabelas
            Divider = "#FFFFFF1F",
            DividerLight = "#FFFFFF0F",
            LinesDefault = "#FFFFFF1F",
            LinesInputs = "#FFFFFF4D",
            TableLines = "#FFFFFF1F",
            TableStriped = "#FFFFFF33",
            TableHover = "#0000000A",
            Skeleton = "#FFFFFF1C",

            // Tons de cinza utilitários
            GrayDefault = "#9E9E9E",
            GrayLight = "#BDBDBD",
            GrayLighter = "#E0E0E0",
            GrayDark = "#757575",
            GrayDarker = "#616161",

            // Overlays e opacidades (dialogs, hover, ripple)
            OverlayDark = "#21212180",
            OverlayLight = "#FFFFFF80",
            BorderOpacity = 1,
            HoverOpacity = 0.06,
            RippleOpacity = 0.1,
            RippleOpacitySecondary = 0.2,
        }
    };
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using CoeurList.Services;
#if ANDROID
using CoeurList.Platforms.Android;
#elif WINDOWS
using CoeurList.Platforms.Windows;
#endif

namespace CoeurList
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddMudServices();
            builder.Services.AddSingleton<ThemeService>();

            builder.Services.AddAuthorizationCore(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });
            builder.Services.AddScoped<AuthenticationStateProvider, CoeurAuthenticationStateProvider>();
#if ANDROID || WINDOWS
            builder.Services.AddSingleton<IGoogleAuthBroker, GoogleAuthBroker>();
#endif
            builder.Services.AddSingleton<AuthService>();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

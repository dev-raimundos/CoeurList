using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using CoeurMobile.App.Core.Config;
using CoeurMobile.App.Core.Http;
using CoeurMobile.App.Core.Services;
using CoeurMobile.App.Modules.Auth.Services;

namespace CoeurMobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<CoeurApplication>()
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
            builder.Services.AddSingleton<TokenAccessor>();
            builder.Services.AddSingleton<ISecureSessionStore, MauiSecureSessionStore>();
            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddTransient<BearerTokenHandler>();
            builder.Services.AddHttpClient<ICoeurApiClient, CoeurApiClient>(client =>
            {
                client.BaseAddress = new Uri(AppConfig.ApiBaseUrl);
            }).AddHttpMessageHandler<BearerTokenHandler>();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

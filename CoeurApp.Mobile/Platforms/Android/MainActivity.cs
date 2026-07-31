using Android.App;
using Android.Content.PM;
using Android.OS;
using AndroidX.Core.View;
using CoeurList.App.Core.Services;

namespace CoeurList.Platforms.Android
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        private ThemeService? _themeService;

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _themeService = IPlatformApplication.Current?.Services.GetService<ThemeService>();
            if (_themeService is not null)
            {
                _themeService.OnChange += UpdateStatusBarAppearance;
                UpdateStatusBarAppearance();
            }
        }

        private void UpdateStatusBarAppearance()
        {
            if (Window is null)
            {
                return;
            }

            var controller = WindowCompat.GetInsetsController(Window, Window.DecorView);
            if (controller is not null)
            {
                controller.AppearanceLightStatusBars = !(_themeService?.IsDarkMode ?? false);
            }
        }

        protected override void OnDestroy()
        {
            if (_themeService is not null)
            {
                _themeService.OnChange -= UpdateStatusBarAppearance;
            }

            base.OnDestroy();
        }
    }
}

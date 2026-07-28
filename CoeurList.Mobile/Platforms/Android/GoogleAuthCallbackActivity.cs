using Android.App;
using Android.Content;
using Android.Content.PM;
using CoeurList.Services;

namespace CoeurList.Platforms.Android;

[Activity(NoHistory = true, LaunchMode = LaunchMode.SingleTop, Exported = true)]
[IntentFilter(
    new[] { Intent.ActionView },
    Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
    DataScheme = GoogleAuthOptions.AndroidRedirectScheme,
    DataHost = GoogleAuthOptions.AndroidRedirectHost)]
public class GoogleAuthCallbackActivity : Microsoft.Maui.Authentication.WebAuthenticatorCallbackActivity
{
}

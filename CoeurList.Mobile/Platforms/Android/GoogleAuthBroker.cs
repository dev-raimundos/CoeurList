using CoeurList.Services;
using Microsoft.Maui.Authentication;

namespace CoeurList.Platforms.Android;

public class GoogleAuthBroker : IGoogleAuthBroker
{
    public string ClientId => GoogleAuthOptions.AndroidClientId;

    public Uri RedirectUri => new($"{GoogleAuthOptions.AndroidRedirectScheme}://{GoogleAuthOptions.AndroidRedirectHost}");

    public async Task<IReadOnlyDictionary<string, string>> AuthenticateAsync(Uri authorizeUri)
    {
        var result = await WebAuthenticator.Default.AuthenticateAsync(authorizeUri, RedirectUri);
        return result.Properties;
    }
}

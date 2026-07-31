namespace CoeurList.App.Core.Config;

public static class AppConfig
{
    public const string ApiBaseUrl = "https://api.coeur.app.br/";

    public const string GoogleClientId = "828733362917-fcq7pkkue5oj0mart4mjjg0jlrr5i0ip.apps.googleusercontent.com";

    public const string GoogleAuthorizationEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";
    public const string GoogleTokenEndpoint = "https://oauth2.googleapis.com/token";

    // Precisa bater com o esquema declarado no IntentFilter de WebAuthenticationCallbackActivity.
    public const string OAuthRedirectScheme = "coeurapp";
    public const string OAuthRedirectUrl = "coeurapp://oauthredirect";
}

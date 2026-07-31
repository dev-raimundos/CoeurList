namespace CoeurList.App.Core.Config;

public static class AppConfig
{
    public const string ApiBaseUrl = "https://api.coeur.app.br/";

    public const string GoogleClientId = "828733362917-fcq7pkkue5oj0mart4mjjg0jlrr5i0ip.apps.googleusercontent.com";

    public const string GoogleAuthorizationEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";
    public const string GoogleTokenEndpoint = "https://oauth2.googleapis.com/token";

    // Exigido pelo Google pra clientes OAuth do tipo Android: o redirect_uri precisa usar o
    // esquema no formato reverso do Client ID (com.googleusercontent.apps.<CLIENT_ID>), não um
    // esquema arbitrário — senão o Google recusa a requisição com "Error 400: invalid_request".
    // Precisa bater com o esquema declarado no IntentFilter de WebAuthenticationCallbackActivity.
    public const string OAuthRedirectScheme = "com.googleusercontent.apps.828733362917-fcq7pkkue5oj0mart4mjjg0jlrr5i0ip";
    public const string OAuthRedirectUrl = "com.googleusercontent.apps.828733362917-fcq7pkkue5oj0mart4mjjg0jlrr5i0ip:/oauth2redirect";
}

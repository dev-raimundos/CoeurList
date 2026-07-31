namespace CoeurList.Config;

public static class AppConfig
{
    // TODO: trocar pelo endereço real do CoeurApi (em dev, o IP da máquina rodando a API
    // — "localhost" não funciona a partir do emulador/dispositivo Android).
    public const string ApiBaseUrl = "https://SEU_HOST_DA_API/";

    // TODO: Client ID OAuth do tipo "Android" criado no Google Cloud Console
    // (Google Cloud Console > APIs & Services > Credentials > Create Credentials > OAuth client ID).
    public const string GoogleClientId = "SEU_CLIENT_ID.apps.googleusercontent.com";

    public const string GoogleAuthorizationEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";
    public const string GoogleTokenEndpoint = "https://oauth2.googleapis.com/token";

    // Precisa bater com o esquema declarado no IntentFilter de WebAuthenticationCallbackActivity.
    public const string OAuthRedirectScheme = "coeurapp";
    public const string OAuthRedirectUrl = "coeurapp://oauthredirect";
}

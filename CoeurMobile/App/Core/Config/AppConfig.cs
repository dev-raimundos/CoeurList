namespace CoeurMobile.App.Core.Config;

public static class AppConfig
{
#if DEBUG
    public const string ApiBaseUrl = "http://10.0.2.2:8000/";
#else
    public const string ApiBaseUrl = "https://api.coeur.app.br/";
#endif
}

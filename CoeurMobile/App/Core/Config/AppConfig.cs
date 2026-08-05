namespace CoeurMobile.App.Core.Config;

public static class AppConfig
{
#if DEBUG
#pragma warning disable S5332
#pragma warning disable S1075
    public const string ApiBaseUrl = "http://10.0.2.2:8000/";
#pragma warning restore S1075
#pragma warning restore S5332
#else
    public const string ApiBaseUrl = "https://api.coeur.app.br/";
#endif
}

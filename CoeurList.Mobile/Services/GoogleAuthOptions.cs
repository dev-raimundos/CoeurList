namespace CoeurList.Services;

/// <summary>
/// Credenciais do OAuth 2.0 do Google. Crie os clients em
/// https://console.cloud.google.com/apis/credentials antes de testar o login:
/// - Android: client tipo "Android", package name "br.app.coeur" e o SHA-1 do keystore de debug/release.
/// - Windows: client tipo "Aplicativo para computador" (Desktop app), que permite redirect_uri http://127.0.0.1:*.
/// </summary>
public static class GoogleAuthOptions
{
    public const string AndroidClientId = "828733362917-i3d4c8gfojkq331g786ae2vlcrohg4dk.apps.googleusercontent.com";

    public const string WindowsClientId = "SUBSTITUA_PELO_DESKTOP_CLIENT_ID.apps.googleusercontent.com";

    public const string AndroidRedirectScheme = "br.app.coeur";

    public const string AndroidRedirectHost = "oauth2redirect";

    public const string Scope = "openid email profile";
}

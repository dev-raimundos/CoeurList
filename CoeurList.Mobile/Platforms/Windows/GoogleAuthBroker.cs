using System.Net;
using System.Text;
using CoeurList.Services;
using Microsoft.Maui.ApplicationModel;

namespace CoeurList.Platforms.Windows;

/// <summary>
/// WebAuthenticator do MAUI depende de ativação por protocolo (URI scheme), que exige um app
/// empacotado (MSIX). Este app roda unpackaged no Windows, então o callback do OAuth é capturado
/// por um redirect_uri de loopback (http://127.0.0.1:PORT), abordagem recomendada pelo próprio
/// Google para apps instalados: https://developers.google.com/identity/protocols/oauth2/native-app.
/// </summary>
public class GoogleAuthBroker : IGoogleAuthBroker
{
    private const int LoopbackPort = 51234;

    public string ClientId => GoogleAuthOptions.WindowsClientId;

    public Uri RedirectUri => new($"http://127.0.0.1:{LoopbackPort}/oauth2redirect/");

    public async Task<IReadOnlyDictionary<string, string>> AuthenticateAsync(Uri authorizeUri)
    {
        using var listener = new HttpListener();
        listener.Prefixes.Add(RedirectUri.ToString());
        listener.Start();

        try
        {
            await Launcher.Default.OpenAsync(authorizeUri);

            var context = await listener.GetContextAsync();
            var parameters = ParseQuery(context.Request.Url?.Query);

            const string html = "<html><body>Login concluído. Você já pode voltar para o CoeurList.</body></html>";
            var buffer = Encoding.UTF8.GetBytes(html);
            context.Response.ContentType = "text/html; charset=utf-8";
            context.Response.ContentLength64 = buffer.Length;
            await context.Response.OutputStream.WriteAsync(buffer);
            context.Response.OutputStream.Close();

            return parameters;
        }
        finally
        {
            listener.Stop();
        }
    }

    private static IReadOnlyDictionary<string, string> ParseQuery(string? query)
    {
        var result = new Dictionary<string, string>();
        if (string.IsNullOrEmpty(query))
        {
            return result;
        }

        foreach (var pair in query.TrimStart('?').Split('&', StringSplitOptions.RemoveEmptyEntries))
        {
            var separatorIndex = pair.IndexOf('=');
            if (separatorIndex < 0)
            {
                result[Uri.UnescapeDataString(pair)] = string.Empty;
                continue;
            }

            var key = Uri.UnescapeDataString(pair[..separatorIndex]);
            var value = Uri.UnescapeDataString(pair[(separatorIndex + 1)..]);
            result[key] = value;
        }

        return result;
    }
}

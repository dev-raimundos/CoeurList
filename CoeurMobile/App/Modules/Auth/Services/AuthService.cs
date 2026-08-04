using System.Net;
using System.Text.Json;
using CoeurMobile.App.Core.Http.Client;
using CoeurMobile.App.Core.Http.Handlers;
using CoeurMobile.App.Modules.Auth.Models;

namespace CoeurMobile.App.Modules.Auth.Services;

public class AuthService
{
    private readonly ICoeurApiClient _apiClient;
    private readonly TokenAccessor _tokenAccessor;
    private readonly ISecureSessionStore _sessionStore;
    private readonly Task _initialization;

    public AuthService(ICoeurApiClient apiClient, TokenAccessor tokenAccessor, ISecureSessionStore sessionStore)
    {
        _apiClient = apiClient;
        _tokenAccessor = tokenAccessor;
        _sessionStore = sessionStore;

        // Se algum request voltar 401, o token salvo está morto — desloga sozinho, sem precisar de
        // interação do usuário. AuthService vive pra sempre (Singleton), então não há necessidade de
        // desinscrever este handler. Precisa vir ANTES de LoadSessionAsync(): a validação da sessão
        // restaurada já pode disparar esse evento antes mesmo do construtor terminar.
        _tokenAccessor.OnUnauthorized += () => _ = LogoutAsync();

        _initialization = LoadSessionAsync();
    }

    public AuthSession? CurrentSession { get; private set; }

    public bool IsAuthenticated()
    {
        return CurrentSession is not null;
    }

    public event Action? OnChange;

    public Task EnsureInitializedAsync()
    {
        return _initialization;
    }

    public async Task LoginAsync(string email, string password)
    {
        var auth = await _apiClient.LoginAsync(email, password);

        CurrentSession = new AuthSession(auth.User.Id, auth.User.Name, auth.User.Email, auth.Token);
        _tokenAccessor.Token = auth.Token;
        await _sessionStore.SetAsync(JsonSerializer.Serialize(CurrentSession));
        OnChange?.Invoke();
    }

    public Task LogoutAsync()
    {
        CurrentSession = null;
        _tokenAccessor.Token = null;
        _sessionStore.Remove();
        OnChange?.Invoke();
        return Task.CompletedTask;
    }

    private async Task LoadSessionAsync()
    {
        AuthSession? session;
        try
        {
            var json = await _sessionStore.GetAsync();
            if (string.IsNullOrEmpty(json))
            {
                return;
            }

            session = JsonSerializer.Deserialize<AuthSession>(json);
        }
        catch
        {
            return;
        }

        if (session is null)
        {
            return;
        }

        _tokenAccessor.Token = session.Token;
        CurrentSession = session;

        try
        {
            // Confirma com a API que o token restaurado ainda é válido. Sem isso, uma sessão expirada ou
            // revogada só seria detectada quando alguma tela chamasse a API — e a Home hoje não chama
            // nenhuma, então o usuário ficaria "logado" indefinidamente com um token morto, só percebendo
            // ao tentar deslogar e logar de novo manualmente.
            await _apiClient.GetMeAsync();
        }
        catch (CoeurApiException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
        {
            // 401 confirmado: o ApiExceptionHandler já disparou TokenAccessor.OnUnauthorized antes de
            // lançar essa exceção, e o handler assinado no construtor já chamou LogoutAsync — não há
            // nada a fazer aqui além de engolir a exceção.
        }
        catch
        {
            // Sem conexão ou erro do servidor não relacionado ao token — mantém a sessão local em vez de
            // forçar logout por um problema que pode ser só temporário.
        }
    }
}

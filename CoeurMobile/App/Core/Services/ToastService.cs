namespace CoeurMobile.App.Core.Services;

/// <summary>Severidade do toast — controla a cor/ícone que o MudBlazor usa pra exibir a mensagem.</summary>
public enum ToastSeverity
{
    Info,
    Warning,
    Error,
}

/// <summary>Mensagem de toast a ser exibida: o texto e a severidade.</summary>
/// <param name="Text">Texto exibido ao usuário (já em português, vindo da API ou de uma mensagem local).</param>
/// <param name="Severity">Severidade usada pra escolher a cor/ícone do toast.</param>
public sealed record ToastMessage(string Text, ToastSeverity Severity);

/// <summary>
/// "Barramento" de toasts: qualquer parte do app (ex.: <see cref="Http.Handlers.ApiExceptionHandler"/>) pode
/// chamar <see cref="Show"/> sem saber quem — ou se alguém — está de fato escutando e desenhando na tela.
/// Quem desenha é o <c>ToastListener</c>, que se inscreve no evento <see cref="OnToast"/>. Registrada como
/// <c>Singleton</c> em <c>MauiProgram.cs</c>, então existe uma única instância compartilhada durante toda a
/// vida do app: quem escreve (<see cref="Show"/>) e quem escuta (<see cref="OnToast"/>) sempre falam com o
/// mesmo objeto — não precisa de interface pra isso, só de identidade compartilhada.
/// </summary>
public sealed class ToastService
{
    /// <summary>
    /// Disparado toda vez que <see cref="Show"/> é chamado. <c>event Action&lt;ToastMessage&gt;?</c> é o
    /// jeito do C# de fazer pub/sub: várias partes do código podem se "inscrever" (com <c>+=</c>) e serem
    /// avisadas quando o evento acontece, sem precisar se conhecer diretamente.
    /// </summary>
    public event Action<ToastMessage>? OnToast;

    /// <summary>Dispara um toast com a mensagem e a severidade informadas (padrão: erro).</summary>
    public void Show(string message, ToastSeverity severity = ToastSeverity.Error)
    {
        // "?." (null-conditional) evita erro caso ninguém tenha se inscrito no evento ainda.
        OnToast?.Invoke(new ToastMessage(message, severity));
    }
}

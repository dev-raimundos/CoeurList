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
/// Contrato de um "barramento" de toasts: qualquer parte do app (ex.:
/// <see cref="Http.Handlers.ApiExceptionHandler"/>) pode chamar <see cref="Show"/> sem saber quem — ou se
/// alguém — está de fato escutando e desenhando na tela. Quem desenha é o <c>ToastListener</c>, que se
/// inscreve no evento <see cref="OnToast"/>.
/// </summary>
public interface IToastService
{
    /// <summary>
    /// Evento disparado toda vez que <see cref="Show"/> é chamado. <c>event Action&lt;ToastMessage&gt;?</c>
    /// é o jeito do C# de fazer pub/sub: várias partes do código podem se "inscrever" (com <c>+=</c>) e serem
    /// avisadas quando o evento acontece, sem precisar se conhecer diretamente.
    /// </summary>
    event Action<ToastMessage>? OnToast;

    /// <summary>Dispara um toast com a mensagem e a severidade informadas (padrão: erro).</summary>
    void Show(string message, ToastSeverity severity = ToastSeverity.Error);
}

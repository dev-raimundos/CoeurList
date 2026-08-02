namespace CoeurMobile.App.Core.Services;

/// <summary>
/// Implementação simples de <see cref="IToastService"/> — só dispara o evento, sem guardar histórico nem
/// fila de mensagens. Registrada como <c>Singleton</c> em <c>MauiProgram.cs</c>, então existe uma única
/// instância compartilhada durante toda a vida do app: quem escreve (<see cref="Show"/>) e quem escuta
/// (<see cref="IToastService.OnToast"/>) sempre falam com o mesmo objeto.
/// </summary>
public sealed class ToastService : IToastService
{
    /// <inheritdoc/>
    public event Action<ToastMessage>? OnToast;

    /// <inheritdoc/>
    public void Show(string message, ToastSeverity severity = ToastSeverity.Error)
    {
        // "?." (null-conditional) evita erro caso ninguém tenha se inscrito no evento ainda.
        OnToast?.Invoke(new ToastMessage(message, severity));
    }
}

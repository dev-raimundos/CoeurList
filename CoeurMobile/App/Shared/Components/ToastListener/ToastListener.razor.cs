using CoeurMobile.App.Core.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CoeurMobile.App.Shared.Components.ToastListener;

/// <summary>
/// Componente Blazor sem nenhum HTML próprio (o <c>.razor</c> correspondente está vazio) — sua única função é
/// fazer a ponte entre o <see cref="ToastService"/> (que não sabe nada de UI) e o <see cref="ISnackbar"/> do
/// MudBlazor (que sabe desenhar o toast na tela). Precisa estar montado em algum lugar da árvore de
/// componentes pra funcionar — por isso foi adicionado no <c>MainLayout</c> e no <c>AuthLayout</c>, os dois
/// layouts usados pelo app.
/// </summary>
public partial class ToastListener : IDisposable
{
    /// <summary>
    /// <c>[Inject]</c> é o jeito do Blazor de pedir uma dependência registrada no container de DI —
    /// equivalente ao construtor injetado que as classes "normais" (fora do mundo de componentes) usam.
    /// </summary>
    [Inject]
    protected ToastService ToastService { get; set; } = default!;

    [Inject]
    protected ISnackbar Snackbar { get; set; } = default!;

    /// <summary>
    /// Chamado pelo Blazor uma única vez, quando o componente é criado. É aqui que a inscrição no evento
    /// acontece — o par de <see cref="Dispose"/>, que desfaz essa inscrição.
    /// </summary>
    protected override void OnInitialized()
    {
        ToastService.OnToast += HandleToast;
    }

    private void HandleToast(ToastMessage toast)
    {
        // InvokeAsync garante que o Snackbar.Add rode na thread de UI do Blazor — o evento pode disparar a
        // partir de uma resposta HTTP que chegou numa thread diferente, e o Blazor não deixa mexer na UI de
        // qualquer thread.
        InvokeAsync(() => Snackbar.Add(toast.Text, ToSeverity(toast.Severity)));
    }

    private static Severity ToSeverity(ToastSeverity severity)
    {
        return severity switch
        {
            ToastSeverity.Warning => Severity.Warning,
            ToastSeverity.Info => Severity.Info,
            _ => Severity.Error,
        };
    }

    /// <summary>
    /// <see cref="IDisposable"/> é o padrão do .NET pra "limpeza" quando um objeto deixa de ser usado. O
    /// Blazor chama <see cref="Dispose"/> automaticamente quando o componente sai da árvore de renderização.
    /// Sem isso, o <see cref="ToastService"/> (que é Singleton, vive pra sempre) ficaria segurando uma
    /// referência a este componente já destruído — um vazamento de memória clássico causado por eventos.
    /// </summary>
    public void Dispose()
    {
        ToastService.OnToast -= HandleToast;
    }
}

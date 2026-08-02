using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;
using PointerEventArgs = Microsoft.AspNetCore.Components.Web.PointerEventArgs;

namespace CoeurMobile.App.Shared.Components.NavMenu;

/// <summary>
/// Menu inferior com dois efeitos visuais: um "indicador" (<c>.nav-indicator</c>) que desliza suavemente
/// até o item da rota atual — mostrando tanto "onde estou" quanto uma transição em bolha entre os links —
/// e um ripple que nasce exatamente no ponto tocado em cada item.
/// </summary>
public partial class NavMenu : IAsyncDisposable
{
    [Inject]
    protected NavigationManager NavigationManager { get; set; } = default!;

    [Inject]
    protected IJSRuntime JS { get; set; } = default!;

    private ElementReference _navRef;
    private ElementReference _indicatorRef;
    private IJSObjectReference? _module;

    private int _rippleIndex = -1;
    private int _rippleX;
    private int _rippleY;

    /// <summary>
    /// Incrementado a cada toque. Usado como <c>@@key</c> do <c>&lt;span class="ripple"&gt;</c> — trocar a
    /// key força o Blazor a destruir o elemento antigo e criar um novo do zero, o que reinicia a animação
    /// CSS mesmo se você tocar duas vezes seguidas no mesmo item.
    /// </summary>
    private int _rippleSeq;

    protected override void OnInitialized()
    {
        NavigationManager.LocationChanged += HandleLocationChanged;
    }

    /// <summary>
    /// Roda depois de CADA render (não só o primeiro) — é o momento certo pra medir o DOM, porque é a
    /// garantia do Blazor de que o navegador já aplicou a última atualização (ex.: a classe <c>.active</c>
    /// que o <see cref="NavLink"/> troca de item sozinho quando a rota muda).
    /// </summary>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _module = await JS.InvokeAsync<IJSObjectReference>("import", "./js/navIndicator.js");
        }

        if (_module is not null)
        {
            await _module.InvokeVoidAsync("updateIndicator", _navRef, _indicatorRef);
        }
    }

    /// <summary>
    /// Só força este componente a re-renderizar (o que aciona <see cref="OnAfterRenderAsync"/> de novo).
    /// O próprio <see cref="NavLink"/> já atualiza sua classe <c>active</c> sozinho quando a rota muda —
    /// aqui a gente só precisa "acordar" pra reposicionar o indicador atrás do novo item ativo.
    /// </summary>
    private void HandleLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        InvokeAsync(StateHasChanged);
    }

    private void HandleRipple(int index, PointerEventArgs e)
    {
        _rippleIndex = index;
        _rippleX = (int)e.OffsetX;
        _rippleY = (int)e.OffsetY;
        _rippleSeq++;
    }

    public async ValueTask DisposeAsync()
    {
        NavigationManager.LocationChanged -= HandleLocationChanged;

        if (_module is not null)
        {
            await _module.DisposeAsync();
        }
    }
}

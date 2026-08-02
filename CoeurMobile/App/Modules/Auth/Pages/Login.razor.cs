using CoeurMobile.App.Core.Services;
using Microsoft.AspNetCore.Components;

namespace CoeurMobile.App.Modules.Auth.Pages;

public partial class Login
{
    [Inject]
    protected AuthService AuthService { get; set; } = default!;

    [Inject]
    protected NavigationManager NavigationManager { get; set; } = default!;

    private bool _isLoading;
    private string? _errorMessage;

    private async Task SubmitAsync()
    {
        _isLoading = true;
        _errorMessage = null;

        try
        {
            var success = await AuthService.LoginWithGoogleAsync();
            if (success)
            {
                NavigationManager.NavigateTo("/", replace: true);
            }
            else
            {
                _errorMessage = "Não foi possível entrar com o Google. Tente novamente.";
            }
        }
        catch (Exception)
        {
            _errorMessage = "Não foi possível entrar com o Google. Tente novamente.";
        }
        finally
        {
            _isLoading = false;
        }
    }
}

using CoeurMobile.App.Core.Http;
using CoeurMobile.App.Modules.Auth.Services;
using CoeurMobile.App.Modules.Auth.Validation;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CoeurMobile.App.Modules.Auth.Pages.Login;

public partial class Login
{
    [Inject]
    protected AuthService AuthService { get; set; } = default!;

    [Inject]
    protected NavigationManager NavigationManager { get; set; } = default!;

    private MudForm _form = null!;
    private string _email = string.Empty;
    private string _password = string.Empty;
    private bool _isPasswordVisible;
    private bool _isLoading;
    private string? _errorMessage;

    private InputType _passwordInputType => _isPasswordVisible ? InputType.Text : InputType.Password;
    private string _passwordAdornmentIcon => _isPasswordVisible ? Icons.Material.Filled.VisibilityOff : Icons.Material.Filled.Visibility;

    private void TogglePasswordVisibility() => _isPasswordVisible = !_isPasswordVisible;

    private static string? ValidateEmail(string email)
        => EmailValidator.HasValidFormat(email) ? null : "Email inválido.";

    private async Task SubmitAsync()
    {
        await _form.ValidateAsync();
        if (!_form.IsValid) return;

        _isLoading = true;
        _errorMessage = null;

        try
        {
            await AuthService.LoginAsync(_email.Trim(), _password);
            NavigationManager.NavigateTo("/", replace: true);
        }
        catch (CoeurApiException ex)
        {
            _errorMessage = ex.Message;
        }
        catch (Exception)
        {
            _errorMessage = "Não foi possível entrar. Tente novamente.";
        }
        finally
        {
            _isLoading = false;
        }
    }
}

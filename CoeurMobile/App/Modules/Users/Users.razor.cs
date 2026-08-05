using CoeurMobile.App.Core.Http.Client;
using CoeurMobile.App.Core.Http.Client.Dtos;
using CoeurMobile.App.Modules.Users.Components.UserDetailsDialog;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CoeurMobile.App.Modules.Users;

public partial class Users
{
    [Inject]
    protected ICoeurApiClient ApiClient { get; set; } = default!;

    [Inject]
    protected IDialogService DialogService { get; set; } = default!;

    // Guarda o UserAccountResponse inteiro (todos os campos que a API manda), mesmo a UI só exibindo o nome —
    // é o que chega da API, não só o que é renderizado.
    private List<UserAccountResponse> _users = [];
    private bool _isLoading = true;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var page = await ApiClient.GetUsersAsync();
            _users = page.Items;
        }
        catch (Exception)
        {
            // O toast com a mensagem de erro (ex.: 403 se o usuário logado não for Admin) já foi
            // disparado pelo ApiExceptionHandler.
        }
        finally
        {
            _isLoading = false;
        }
    }

    private async Task ShowUserDetailsAsync(UserAccountResponse user)
    {
        var parameters = new DialogParameters<UserDetailsDialog>
        {
            { x => x.User, user }
        };

        await DialogService.ShowAsync<UserDetailsDialog>("Detalhes do usuário", parameters);
    }
}

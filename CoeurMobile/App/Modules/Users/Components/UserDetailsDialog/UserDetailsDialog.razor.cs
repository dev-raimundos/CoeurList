using CoeurMobile.App.Modules.Users.Dtos;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CoeurMobile.App.Modules.Users.Components.UserDetailsDialog;

public partial class UserDetailsDialog
{
    [CascadingParameter]
    private IMudDialogInstance MudDialog { get; set; } = default!;

    [Parameter]
    public UserAccountResponse User { get; set; } = default!;

    private void Close()
    {
        MudDialog.Close();
    }
}

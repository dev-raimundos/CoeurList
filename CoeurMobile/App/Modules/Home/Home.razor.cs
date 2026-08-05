using CoeurMobile.App.Core.Services;
using Microsoft.AspNetCore.Components;

namespace CoeurMobile.App.Modules.Home;

public partial class Home
{
    [Inject]
    protected IAuthService AuthService { get; set; } = default!;
}

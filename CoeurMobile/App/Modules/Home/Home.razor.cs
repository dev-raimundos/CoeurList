using CoeurMobile.App.Core.Services;
using Microsoft.AspNetCore.Components;

namespace CoeurMobile.App.Modules.Home;

public partial class Home
{
    [Inject]
    protected AuthService AuthService { get; set; } = default!;
}

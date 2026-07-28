using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace CoeurList.Services;

public class CoeurAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly AuthService _authService;

    public CoeurAuthenticationStateProvider(AuthService authService)
    {
        _authService = authService;
        _authService.OnChange += () => NotifyAuthenticationStateChanged(BuildAuthenticationStateAsync());
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        await _authService.EnsureInitializedAsync();
        return BuildAuthenticationState();
    }

    private Task<AuthenticationState> BuildAuthenticationStateAsync() => Task.FromResult(BuildAuthenticationState());

    private AuthenticationState BuildAuthenticationState()
    {
        if (!_authService.IsAuthenticated || _authService.CurrentSession is not { } session)
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        var identity = new ClaimsIdentity(
            new[]
            {
                new Claim(ClaimTypes.Email, session.Email),
                new Claim(ClaimTypes.Name, session.Email),
            },
            authenticationType: "CoeurList");

        return new AuthenticationState(new ClaimsPrincipal(identity));
    }
}

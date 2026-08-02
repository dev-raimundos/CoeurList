namespace CoeurMobile.App.Modules.Auth.Services;

public sealed record AuthSession(Guid UserId, string Name, string Email, string Token);

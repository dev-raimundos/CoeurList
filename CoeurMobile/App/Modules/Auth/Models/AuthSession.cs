namespace CoeurMobile.App.Modules.Auth.Models;

public sealed record AuthSession(Guid UserId, string Name, string Email, string Token);

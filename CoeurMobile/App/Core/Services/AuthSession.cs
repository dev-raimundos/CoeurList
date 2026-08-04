namespace CoeurMobile.App.Core.Services;

public sealed record AuthSession(Guid UserId, string Name, string Email, string Token);

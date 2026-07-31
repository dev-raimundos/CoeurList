namespace Coeur.Mobile.Application.Authentication;

public sealed record AuthSession(Guid UserId, string Name, string Email, string Token);

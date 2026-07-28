namespace Coeur.Mobile.Application.Authentication;

public sealed record AuthSession(
    string UserId,
    string Email,
    string Name,
    string PictureUrl,
    string IdToken,
    string? AccessToken,
    DateTimeOffset ExpiresAtUtc);

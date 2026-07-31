namespace Coeur.Mobile.Application.DTOs.Responses;

public sealed record UserResponse(Guid Id, string Name, string Email);

public sealed record AuthResponse(UserResponse User, string Token);

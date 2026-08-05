namespace CoeurMobile.App.Modules.Auth.Dtos;

public sealed record UserResponse(Guid Id, string Name, string Email);

public sealed record AuthResponse(UserResponse User, string Token);

namespace CoeurMobile.App.Core.Http.Client.Dtos;

public sealed record UserAccountResponse(
    Guid Id,
    string Name,
    string Email,
    string Role,
    bool IsActive,
    bool IsEmailVerified,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    DateTime? LastLoginAt
);

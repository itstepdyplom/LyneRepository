namespace Lyne.Application.DTO.Auth;

public sealed class UserProfileDto
{
    public long   Id        { get; init; }
    public string Email     { get; init; } = default!;
    public string Name      { get; init; } = default!;
    public string ForName   { get; init; } = default!;
    public string Role      { get; init; } = default!;
    public DateTimeOffset CreatedAt { get; init; }

    public static UserProfileDto From(Lyne.Domain.Entities.User u) => new()
    {
        Id = u.Id,
        Email = u.Email,
        Name = u.Name,
        ForName = u.ForName,
        Role = u.Role,
        CreatedAt = u.CreatedAt
    };
}
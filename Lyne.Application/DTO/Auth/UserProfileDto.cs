using Lyne.Domain.Entities;

namespace Lyne.Application.DTO.Auth;

public sealed class UserProfileDto
{
    public long   Id        { get; init; }
    public string Email     { get; init; } = default!;
    public string Name      { get; init; } = default!;
    public string ForName   { get; init; } = default!;
    public string Role      { get; init; } = default!;
    public DateTimeOffset CreatedAt { get; init; }
    public int? AddressId { get; set; }
    public AddressDto? Address { get; set; }

    public static UserProfileDto From(User u) => new()
    {
        Id = u.Id,
        Email = u.Email,
        Name = u.Name,
        ForName = u.ForName,
        Role = u.Role,
        CreatedAt = u.CreatedAt,

        AddressId = u.AddressId,
        Address = u.Address == null ? null : new AddressDto
        {
            Id = u.Address.Id,
            Street = u.Address.Street,
            City = u.Address.City,
            State = u.Address.State,
            Country = u.Address.Country,
            Zip = u.Address.Zip,
        }
    };
}
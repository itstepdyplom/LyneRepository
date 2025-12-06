namespace Lyne.Application.DTO.Auth;

public class UserUpdateDto
{
    public string? Name { get; set; }
    public string? ForName { get; set; }
    public string? Gender { get; set; }
    public string? PhoneNumber { get; set; }
    public string? DateOfBirth { get; set; }
    public AddressDto? Address { get; set; }
}
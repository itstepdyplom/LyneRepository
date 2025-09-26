using System.ComponentModel.DataAnnotations;

namespace Lyne.Application.DTO;

public class RegisterUserDto
{
    
    [Required(ErrorMessage = "Name is required"), MaxLength(50)]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "ForName is required"), MaxLength(50)]
    public string ForName { get; set; } = "";

    public string Gender { get; set; } = "";

    public DateTime DateOfBirth { get; set; }

    [Required(ErrorMessage = "PhoneNumber is required"),Phone]
    public string? PhoneNumber { get; set; }

    [Required(ErrorMessage = "Email is required"), EmailAddress]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = "";
}

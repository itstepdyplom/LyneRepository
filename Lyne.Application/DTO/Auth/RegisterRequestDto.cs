using System.ComponentModel.DataAnnotations;

namespace Lyne.Application.DTO.Auth;

public class RegisterRequestDto
{
    [Required]
    public required string Name { get; set; }
    
    [Required]
    public required string ForName { get; set; }
    
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
    
    [Required]
    [MinLength(6)]
    public required string Password { get; set; }
    
    [Required]
    [Compare("Password")]
    public required string ConfirmPassword { get; set; }
    
    public string? Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? PhoneNumber { get; set; }
} 
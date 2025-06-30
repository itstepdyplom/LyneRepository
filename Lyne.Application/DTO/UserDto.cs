using System.ComponentModel.DataAnnotations;
using Lyne.Domain.Entities;

namespace Lyne.Application.DTO;

public class UserDto
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Name is required!"), MaxLength(50)]
    public string? Name { get; set; }
    [Required(ErrorMessage = "ForName is required!"), MaxLength(50)]
    public string? ForName { get; set; }
    [Required(ErrorMessage = "Genre is required!"), MaxLength(10)]
    public string? Genre { get; set; }
    
    [Required]
    public string? PasswordHash { get; set; }
    public DateTime? DateOfBirth { get; set; }
    
    [Required(ErrorMessage = "PhoneNumber is required!"),Phone]
    public string? PhoneNumber { get; set; }
    
    [Required(ErrorMessage = "Email is required!"),EmailAddress]
    public string? Email { get; set; }
    
    public AddressDto? Address { get; set; }
    
    public ICollection<int> OrderIds { get; set; } = new List<int>();
}

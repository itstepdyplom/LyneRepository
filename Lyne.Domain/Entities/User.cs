using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lyne.Domain.Entities;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // важливо
    public int Id { get; set; }
    public required string Name { get; set; } = "";
    public required string ForName { get; set; } = "";
    public required string Gender { get; set; }
    public required string PasswordHash { get; set; } = "";
    public DateOnly DateOfBirth { get; set; }

    [Phone]
    public string? PhoneNumber { get; set; }

    [EmailAddress]
    public required string Email { get; set; } = "";
    public int? AddressId { get; set; }

    [ForeignKey("AddressId")]
    public Address? Address { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public required string Role { get; set; }
    
    public ICollection<Order>? Orders { get; set; } = new List<Order>();
}

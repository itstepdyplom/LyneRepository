using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lyne.Domain.Entities;

public class User
{
    [Key]
    public required int Id { get; set; }
    public required string Name { get; set; } = "";
    public required string ForName { get; set; } = "";
    public string Genre { get; set; }
    public required string PasswordHash { get; set; } = "";
    public DateTime DateOfBirth { get; set; }

    [Phone]
    public string? PhoneNumber { get; set; }

    [EmailAddress]
    public required string Email { get; set; } = "";
    public int AddressId { get; set; }

    [ForeignKey("AddressId")]
    public Address? Address { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public ICollection<Order>? Orders { get; set; } = new List<Order>();
}

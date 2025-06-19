using System.ComponentModel.DataAnnotations;

namespace Lyne.Domain.Entities;

public class Address
{
    [Key]
    public required int Id { get; set; }
    public required string Street { get; set; }
    public required string City { get; set; }
    public required string State { get; set; }
    public string Zip { get; set; }
    public required string Country { get; set; }
    public User? User { get; set; }
}

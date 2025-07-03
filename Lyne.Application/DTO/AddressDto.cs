using System.ComponentModel.DataAnnotations;
using Lyne.Domain.Entities;

namespace Lyne.Application.DTO;

public class AddressDto
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Street is required!")]
    public string? Street { get; set; }
    [Required(ErrorMessage = "City is required!")]
    public string? City { get; set; }
    public required string State { get; set; }
    [Required(ErrorMessage = "Country is required!")]
    public string? Country { get; set; }
    public string? Zip { get; set; }
    public int UserId { get; set; }
}

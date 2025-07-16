using Lyne.Domain.Entities;
using System.ComponentModel.DataAnnotations;
namespace Lyne.Application.DTO;

public class ProductDto
{
    public Guid Id { get; set; }
    [Required(ErrorMessage = "Name is required!"), MaxLength(100)]
    public string Name { get; set; } = "";
    [Required(ErrorMessage = "Brand is required!"), MaxLength(100)]
    public string Brand { get; set; } = "";
    [Required(ErrorMessage = "Price is required!")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0!")]
    public decimal Price { get; set; }
    [Required(ErrorMessage = "Category is required!")]
    public Guid CategoryId { get; set; }
    [MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters!")]
    public string Description { get; set; } = "";
    [Required(ErrorMessage = "Stock quantity required!")]
    [Range(0, int.MaxValue, ErrorMessage = "The quantity cannot be negative.")]
    public int StockQuantity { get; set; }
    [Url(ErrorMessage = "Invalid image URL format")]
    public string ImageUrl { get; set; } = "";
    [MaxLength(50)]
    public string Size { get; set; } = "";
    [MaxLength(50)]
    public string Color { get; set; } = "";
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

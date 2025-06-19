using Lyne.Domain.Entities;

namespace Lyne.Application.DTO;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Brand { get; set; } = "";
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
    public string Description { get; set; } = "";
    public int StockQuantity { get; set; }
    public string ImageUrl { get; set; } = "";
    public string Size { get; set; } = "";
    public string Color { get; set; } = "";
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

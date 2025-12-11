using System.ComponentModel.DataAnnotations;

namespace Lyne.Domain.Entities;

public class Category
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public List<Product> Products { get; set; } = new List<Product>();
}

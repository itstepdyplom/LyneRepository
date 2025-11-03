using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Lyne.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Brand { get; set; }= "";
    [Range(0,double.MaxValue)]
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
    
    [ForeignKey("CategoryId")]
    public Category? Category { get; set; }
    
    public string Description { get; set; } = "";
    public int StockQuantity { get; set; }
    public string ImageUrl { get; set; } = "";
    public string Size { get; set; } = "";
    public string Color { get; set; } = "";
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
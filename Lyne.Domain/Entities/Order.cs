using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lyne.Domain.Enums;

namespace Lyne.Domain.Entities;

public class Order
{
    [Key]
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public int UserId { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; } = null!;

    public int ShippingAddressId { get; set; }

    [ForeignKey("ShippingAddressId")] 
    public Address ShippingAddress { get; set; } = null!;

    public string PaymentMethod { get; set; } = "";
    public int TrackingNumber { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
}

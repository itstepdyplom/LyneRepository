using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lyne.Domain.Entities;
using Lyne.Domain.Enums;

namespace Lyne.Application.DTO;

public class OrderDto
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Date is required")]
    public required DateTime Date { get; set; }
    public int UserId { get; set; }
    public int ShippingAddressId { get; set; }
    public string PaymentMethod { get; set; } = "";
    public int TrackingNumber { get; set; }
    [Required(ErrorMessage = "OrderStatus is required")]
    public required OrderStatus OrderStatus { get; set; }
    public List<int> ProductIds { get; set; } = new();
}

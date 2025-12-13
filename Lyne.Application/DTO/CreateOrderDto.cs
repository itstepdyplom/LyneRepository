namespace Lyne.Application.DTO;

public class CreateOrderDto
{
    public int ShippingAddressId { get; set; }
    public string PaymentMethod { get; set; } = default!;
    public List<CreateOrderItemDto> Items { get; set; } = [];
}
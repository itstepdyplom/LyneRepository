using AutoMapper;
using Lyne.Application.DTO;
using Lyne.Application.Services;
using Lyne.Domain.Entities;
using Lyne.Domain.Enums;
using Lyne.Domain.IRepositories;
using Microsoft.Extensions.Logging;

namespace Lyne.Infrastructure.Services;

public class OrderService(IOrderRepository orderRepository,IMapper mapper,ILogger<OrderService> logger):IOrderService
{
    public async Task<List<OrderDto>> GetAllAsync()
    {
        logger.LogInformation("Getting all orders");
        var orders = await orderRepository.GetAllAsync();
        if (orders.Count == 0)
        {
            logger.LogInformation("No orders found");
        }
        return mapper.Map<List<OrderDto>>(orders);
    }

    public async Task<OrderDto?> GetByIdAsync(int id)
    {
        logger.LogInformation("Getting order by id");
        var order = await orderRepository.GetByIdAsync(id);
        return mapper.Map<OrderDto>(order);
    }

    public async Task<bool> AddAsync(CreateOrderDto dto, int userId)
    {
        if (!dto.Items.Any()) return false;

        var order = new Order
        {
            UserId = userId,
            Date = DateTimeOffset.UtcNow,
            OrderStatus = OrderStatus.Pending,

            ShippingAddressId = dto.ShippingAddressId,
            PaymentMethod = dto.PaymentMethod,

            OrderProducts = dto.Items.Select(i => new OrderProduct
            {
                ProductId = i.ProductId,   // Guid
                Quantity  = i.Quantity,    // int
                UnitPrice = i.UnitPrice    // decimal
            }).ToList()
        };

        await orderRepository.AddAsync(order);
        return true;
    }

    public async Task<bool> UpdateAsync(OrderDto dto)
    {
        logger.LogInformation("Updating order");
        if(dto==null) return false;
        var order = mapper.Map<Order>(dto);
        order.UpdatedAt = DateTime.UtcNow;

        return await orderRepository.Update(order);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        logger.LogInformation("Deleting order");
        var orderDto = await GetByIdAsync(id);
        if (orderDto == null)
            return false;
        var order = mapper.Map<Order>(orderDto);
        return await orderRepository.DeleteAsync(order);
    }
    public async Task<List<OrderDto>> GetMyOrdersAsync(long userId, CancellationToken ct)
    {
        logger.LogInformation("Getting orders for user {userId}",userId);
        var orders = await orderRepository.GetMyOrdersAsync(userId, ct);
        return mapper.Map<List<OrderDto>>(orders);
    }
}

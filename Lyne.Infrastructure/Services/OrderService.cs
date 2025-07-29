using AutoMapper;
using Lyne.Application.DTO;
using Lyne.Application.Services;
using Lyne.Domain.Entities;
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

    public async Task<bool> AddAsync(OrderDto dto)
    {
        try
        {
            var order = mapper.Map<Order>(dto);
            if (!await orderRepository.ValidateForCreateAsync(order))
                return false;
            return await orderRepository.AddAsync(order);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error adding order");
            return false;
        }
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
}

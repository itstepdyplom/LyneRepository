using AutoMapper;
using Lyne.Application.DTO;
using Lyne.Domain.Entities;
using Lyne.Domain.IRepositories;

namespace Lyne.Application.Services;

public class OrderService(IOrderRepository orderRepository,IMapper mapper):IOrderService
{
    public async Task<List<OrderDto>> GetAllAsync()
    {
        var orders = await orderRepository.GetAllAsync();
        return mapper.Map<List<OrderDto>>(orders);
    }

    public async Task<OrderDto?> GetByIdAsync(int id)
    {
        var order = await orderRepository.GetByIdAsync(id);
        return mapper.Map<OrderDto>(order);
    }

    public async Task<bool> AddAsync(OrderDto dto)
    {
        var order = mapper.Map<Order>(dto);
        order.CreatedAt = DateTime.UtcNow;
        order.UpdatedAt = DateTime.UtcNow;

        if (!await orderRepository.ValidateForCreateAsync(order))
            return false;
    
        await orderRepository.AddAsync(order);
        return true;
    }

    public async Task<bool> UpdateAsync(OrderDto dto)
    {
        if (!await orderRepository.ExistsAsync(dto.Id))
            return false;
        
        var order = mapper.Map<Order>(dto);
        order.UpdatedAt = DateTime.UtcNow;
        
        if (!await orderRepository.ValidateForUpdateAsync(order))
            return false;

        await orderRepository.Update(order);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var order = await orderRepository.GetByIdAsync(id);
        if (order == null) return false;

        await orderRepository.DeleteAsync(order);
        return true;
    }
}

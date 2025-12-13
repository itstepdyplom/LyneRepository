using Lyne.Application.DTO;
using Lyne.Domain.Entities;

namespace Lyne.Application.Services;

public interface IOrderService
{
    public Task<List<OrderDto>> GetAllAsync();

    public Task<OrderDto?> GetByIdAsync(int id);

    public Task<bool> AddAsync(CreateOrderDto dto, int userId);

    public Task<bool> UpdateAsync(OrderDto dto);

    public Task<bool> DeleteAsync(int id);
    Task<List<OrderDto>> GetMyOrdersAsync(long userId, CancellationToken ct);

}

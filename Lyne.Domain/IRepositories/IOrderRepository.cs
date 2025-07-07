using Lyne.Domain.Entities;

namespace Lyne.Domain.IRepositories;

public interface IOrderRepository
{
    Task<List<Order>> GetAllAsync();
    Task<Order?> GetByIdAsync(int id);
    Task<bool> AddAsync(Order? order);
    Task<bool> Update(Order? order);
    Task<bool> DeleteAsync(Order? order);
    Task<bool> ExistsAsync(int id);
    Task<bool> ValidateForCreateAsync(Order order);
    Task<bool> ValidateForUpdateAsync(Order? order);
}

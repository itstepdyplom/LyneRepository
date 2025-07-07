using Lyne.Domain.Entities;

namespace Lyne.Domain.IRepositories;

public interface IProductRepository
{
    Task<List<Product?>> GetAllAsync();
    Task<Product?> GetByIdAsync(Guid id);
    Task<bool> AddAsync(Product? product);
    Task<bool> Update(Product? product);
    Task<bool> DeleteAsync(Product? product);
    Task<bool> ExistsAsync(Guid id);
    Task<bool> ValidateForCreateAsync(Product product);
    Task<bool> ValidateForUpdateAsync(Product? product);
}

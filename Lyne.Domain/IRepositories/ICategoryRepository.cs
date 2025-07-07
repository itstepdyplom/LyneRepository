using Lyne.Domain.Entities;

namespace Lyne.Domain.IRepositories;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(Guid id);
    Task<bool> AddAsync(Category? category);
    Task<bool> Update(Category? category);
    Task<bool> DeleteAsync(Category? category);
    Task<bool> ExistsAsync(Guid id);
    Task<bool> ValidateForCreateAsync(Category category);
    Task<bool> ValidateForUpdateAsync(Category? category);
}

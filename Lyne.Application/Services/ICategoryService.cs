using Lyne.Application.DTO;

namespace Lyne.Application.Services;

public interface ICategoryService
{
    public Task<List<CategoryDto>> GetAllAsync();

    public Task<CategoryDto?> GetByIdAsync(Guid id);

    public Task<bool> AddAsync(CategoryDto dto);

    public Task<bool> UpdateAsync(CategoryDto dto);

    public Task<bool> DeleteAsync(Guid id);
}

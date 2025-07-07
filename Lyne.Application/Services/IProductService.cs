using Lyne.Application.DTO;

namespace Lyne.Application.Services;

public interface IProductService
{
    public Task<List<ProductDto>> GetAllAsync();

    public Task<ProductDto?> GetByIdAsync(Guid id);

    public Task<bool> AddAsync(ProductDto dto);

    public Task<bool> UpdateAsync(ProductDto? dto);

    public Task<bool> DeleteAsync(Guid id);
}

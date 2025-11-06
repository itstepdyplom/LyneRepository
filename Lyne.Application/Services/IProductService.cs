using Lyne.Application.DTO;
using Lyne.Domain.Entities;

namespace Lyne.Application.Services;

public interface IProductService
{
    public Task<List<ProductDto>> GetAllAsync();

    public Task<ProductDto?> GetByIdAsync(Guid id);

    public Task<(bool, Product created)> AddAsync(ProductDto dto);

    public Task<bool> UpdateAsync(ProductDto? dto);

    public Task<bool> DeleteAsync(Guid id);
}

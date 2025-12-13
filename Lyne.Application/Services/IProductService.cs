using Lyne.Application.DTO;
using Lyne.Domain.Entities;

namespace Lyne.Application.Services;

public interface IProductService
{
    public Task<List<ProductDto>> GetAllAsync();
    Task<(List<ProductDto> items, int total)> GetPagedAsync(int page, int limit, string? categoryName);
    public Task<ProductDto?> GetByIdAsync(Guid id);

    public Task<(bool, Product created)> AddAsync(ProductDto dto);

    public Task<bool> UpdateAsync(ProductDto? dto);

    public Task<bool> DeleteAsync(Guid id);
}

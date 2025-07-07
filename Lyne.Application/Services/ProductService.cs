using AutoMapper;
using Lyne.Application.DTO;
using Lyne.Domain.Entities;
using Lyne.Domain.IRepositories;

namespace Lyne.Application.Services;

public class ProductService(IProductRepository productRepository,IMapper mapper):IProductService
{
    public async Task<List<ProductDto>> GetAllAsync()
    {
        var products = await productRepository.GetAllAsync();
        return mapper.Map<List<ProductDto>>(products);
    }

    public async Task<ProductDto?> GetByIdAsync(Guid id)
    {
        var product = await productRepository.GetByIdAsync(id);
        return mapper.Map<ProductDto>(product);
    }

    public async Task<bool> AddAsync(ProductDto dto)
    {
        var product = mapper.Map<Product>(dto);
        product.CreatedAt = DateTime.UtcNow;
        product.UpdatedAt = DateTime.UtcNow;

        if (!await productRepository.ValidateForCreateAsync(product))
            return false;
    
        await productRepository.AddAsync(product);
        return true;
    }

    public async Task<bool> UpdateAsync(ProductDto? dto)
    {
        if (dto==null)
            return false;
        
        if (!await productRepository.ExistsAsync(dto.Id))
            return false;
        
        var product = mapper.Map<Product>(dto);
        product.UpdatedAt = DateTime.UtcNow;
        
        if (!await productRepository.ValidateForUpdateAsync(product))
            return false;

        await productRepository.Update(product);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var product = await productRepository.GetByIdAsync(id);
        if (product == null) return false;

        await productRepository.DeleteAsync(product);
        return true;
    }
}

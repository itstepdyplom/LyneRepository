using AutoMapper;
using Lyne.Application.DTO;
using Lyne.Application.Services;
using Lyne.Domain.Entities;
using Lyne.Domain.IRepositories;
using Microsoft.Extensions.Logging;

namespace Lyne.Infrastructure.Services;

public class ProductService(IProductRepository productRepository,IMapper mapper,ILogger<ProductService> logger):IProductService
{
    public async Task<List<ProductDto>> GetAllAsync()
    {
        logger.LogInformation("Getting all products");
        var products = await productRepository.GetAllAsync();
        if (products == null || products.Count == 0)
        {
            logger.LogInformation("No products");
            return new List<ProductDto>();
        }
        return mapper.Map<List<ProductDto>>(products);
    }

    public async Task<ProductDto?> GetByIdAsync(Guid id)
    {
        logger.LogInformation("Getting product with id:{Id}", id);
        var product = await productRepository.GetByIdAsync(id);
        return mapper.Map<ProductDto>(product);
    }

    public async Task<(bool,Product created)> AddAsync(ProductDto dto)
    {
            logger.LogInformation("Adding product");
            var p = mapper.Map<Product>(dto);
            //p.Id= Guid.NewGuid();
            //p.Id = p.Id == Guid.Empty ? Guid.NewGuid() : p.Id;
            p.CreatedAt = DateTime.UtcNow;
            p.UpdatedAt = DateTime.UtcNow;
    
            var ok = await productRepository.AddAsync(p);
            return (ok, p);
    }

    public async Task<bool> UpdateAsync(ProductDto? dto)
    {
        if (dto == null)
        {
            logger.LogWarning("Cannot update product, dto is null");
            return false;
        }

        logger.LogInformation("Updating product with name: {name}, brand: {brand}", dto.Name, dto.Brand);
        var product = mapper.Map<Product>(dto);
        if (product == null)
        {
            logger.LogWarning("Cannot update product, mapping failed");
            return false;
        }
        product.UpdatedAt = DateTime.UtcNow;

        return await productRepository.Update(product);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        logger.LogInformation("Deleting product with id:{Id}", id);
        var productDto = await GetByIdAsync(id);
        var product = mapper.Map<Product>(productDto);
        return await productRepository.DeleteAsync(id);
    }
}

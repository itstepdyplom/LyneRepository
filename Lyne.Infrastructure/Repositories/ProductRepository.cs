using System.Data;
using System.Globalization;
using AutoMapper;
using Lyne.Application.DTO;
using Lyne.Domain.Entities;
using Lyne.Domain.IRepositories;
using Lyne.Infrastructure.Caching;
using Lyne.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Npgsql;
using Supabase.Interfaces;

namespace Lyne.Infrastructure.Repositories;

public class ProductRepository(AppDbContext context, ILogger<ProductRepository> logger,ICacheService cacheService,IMapper mapper): IProductRepository
{
    private const string AllProductsKey = "products:all";

    public async Task<List<Product?>> GetAllAsync()
    {
        var cached = await cacheService.GetAsync<List<Product>>(AllProductsKey);
        if (cached is not null && cached.Count > 0)
            return cached;
        var products = await context.Products.ToListAsync();
        await cacheService.SetRangeAsync(AllProductsKey, products, "products", TimeSpan.FromMinutes(15));
        return (await Task.FromResult(products))!;
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        var cacheKey = $"product:{id}";
        var product = await cacheService.GetAsync<Product>(cacheKey);
        if (product is null)
        {
            product = await context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product is not null)
            {
                logger.LogInformation("Product id:{Id} was found", product.Id);
                await cacheService.SetAsync(cacheKey, product, "product", TimeSpan.FromMinutes(15));
            }
            else
            {
                logger.LogWarning("Product with id:{Id} was not found", id);
                return null;
            }
        }

        return product;
    }

    public async Task<(List<Product> items, int total)> GetPagedAsync(int page, int limit, string? categoryName)
    {
        var query = context.Products
            .Include(p => p.Category)  
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(categoryName))
        {
            var name = categoryName.Trim().ToLower();
            query = query.Where(p =>
                p.Category != null && p.Category.Name.ToLower() == name);
        }

        var total = await query.CountAsync();

        var items = await query
            .OrderByDescending(p => p.CreatedAt)
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToListAsync();

        return (items, total);
    }

    public async Task<bool> AddAsync(Product? product)
    {
        if (product is null)
        {
            logger.LogInformation("Cannot add product with id:{Id}, some fields are empty", product!.Id);
            return false;
        }
        if (!await ValidateForCreateAsync(product))
        {
            logger.LogInformation("Cannot add product with id:{Id}, validation issues", product!.Id);
            return false;
        }

        // product.Id = Guid.Empty;
        
         var result = await context.Products.AddAsync(product);
         await context.SaveChangesAsync();
         
         return result is not null ? true : false;
    }

    public async Task<bool> Update(Product? product, CancellationToken ct = default)
    {
        if (product is null)
        {
            logger.LogWarning("Attempted to add a null product");
            return false;
        }

        var existing = await context.Products.FindAsync(product.Id);
        if (existing is null)
        {
            logger.LogWarning("Product with ID {Id} not found", product.Id);
            return false;
        }
        if (!await ValidateForUpdateAsync(product))
        {
            logger.LogInformation("Cannot update product with id:{Id}, validation issues", product!.Id);
            return false;
        }

        mapper.Map(product, existing);
        await context.SaveChangesAsync();

        await cacheService.RemoveAsync("products:all", "product");
        await cacheService.SetAsync($"product:{product.Id}", product, "product", TimeSpan.FromMinutes(15));
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        // if (product is null)
        // {
        //     logger.LogWarning("Product is null");
        //     return false;
        // }

        // var existing = await context.Products.FindAsync(product.Id);
        // if (existing is null)
        // {
        //     logger.LogWarning("Product not found with id {Id}", product.Id);
        //     return false;
        // }

        // if (!await ValidateForUpdateAsync(product))
        // {
        //     logger.LogInformation("Cannot delete product with id:{Id}, validation issues", product.Id);
        //     return false;
        // }

        await context.Products.Where(p => p.Id == id).ExecuteDeleteAsync();
        await context.SaveChangesAsync();
        
        var cacheKey = $"product:{id}";
        await cacheService.RemoveAsync(cacheKey,"product"); 
        logger.LogInformation("Product with id:{Id} deleted", id);
        return true;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        var product = await context.Products.FindAsync(id);
        if (product is not null)
            logger.LogInformation("Product with id:{Id} not exists", product!.Id);
        
        logger.LogInformation("Getting product with id:{Id}", id);
        return product is not null ? true : false;
    }

    public async Task<bool> ValidateForCreateAsync(Product product)
    {
        bool isValid = !string.IsNullOrEmpty(product.Brand) &&
                       !string.IsNullOrEmpty(product.Color) &&
                       !string.IsNullOrEmpty(product.Description) &&
                       !string.IsNullOrEmpty(product.ImageUrl) &&
                       !string.IsNullOrEmpty(product.Name) &&
                       product.IsActive &&
                       product.Price > 0 &&
                       product.StockQuantity >= 0;

        logger.LogInformation("ValidateForCreateProductAsync: Validation {Result}", isValid ? "passed" : "failed");
        return isValid;
    }

    public async Task<bool> ValidateForUpdateAsync(Product? product)
    {
        Npgsql.NpgsqlConnection.ClearAllPools();
        var productExists = await context.Products.AnyAsync(u => u.Id == product!.Id);

        bool isValid = !string.IsNullOrEmpty(product?.Brand) &&
                       !string.IsNullOrEmpty(product.Color) &&
                       !string.IsNullOrEmpty(product.Description) &&
                       !string.IsNullOrEmpty(product.ImageUrl) &&
                       !string.IsNullOrEmpty(product.Name) &&
                       !string.IsNullOrEmpty(product.IsActive.ToString()) &&
                       !string.IsNullOrEmpty(product.Price.ToString(CultureInfo.InvariantCulture)) &&
                       !string.IsNullOrEmpty(product.StockQuantity.ToString()) &&
                       productExists;

        logger.LogInformation("ValidateForUpdateProductAsync: Validation {Result}", isValid ? "passed" : "failed");
        return isValid;
    }
}

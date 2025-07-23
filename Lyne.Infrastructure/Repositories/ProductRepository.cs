using System.Globalization;
using Lyne.Domain.Entities;
using Lyne.Domain.IRepositories;
using Lyne.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace Lyne.Infrastructure.Repositories;

public class ProductRepository(AppDbContext context, ILogger<ProductRepository> logger): IProductRepository
{
    public async Task<List<Product?>> GetAllAsync()
    {
        var products = await context.Products.ToListAsync();
        return (await Task.FromResult(products))!;
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        var product = await context.Products.FirstOrDefaultAsync(x => x.Id == id);

        if (product == null)
        {
            logger.LogWarning("Product with id:{Id} was not found", id);
            return null;
        }

        logger.LogInformation("Product id:{Id} was found", product.Id);
        return product;
    }

    public async Task<bool> AddAsync(Product? product)
    {
        if (product == null)
        {
            logger.LogInformation("Cannot add product with id:{Id}, some fields are empty", product!.Id);
            return false;
        }
        if (!await ValidateForCreateAsync(product))
        {
            logger.LogInformation("Cannot add product with id:{Id}, validation issues", product!.Id);
            return false;
        }
        
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();
        logger.LogInformation("Product with id:{Id} added", product!.Id);
        return true;
    }

    public async Task<bool> Update(Product? product)
    {
        if (product is null)
        {
            logger.LogWarning("Attempted to add a null product");
            return false;
        }
        var existing = await context.Products.FindAsync(product.Id);
        if (existing == null)
        {
            logger.LogWarning("Product with ID {Id} not found", product.Id);
            return false;
        }
        if (!ValidateForUpdateAsync(product).Result)
        {
            logger.LogInformation("Cannot update product with id:{Id}, validation issues", product!.Id);
            return false;
        }
        
        context.Products.Update(existing);
        await context.SaveChangesAsync();
        logger.LogInformation("Product with id:{Id} updated", product!.Id);
        return await Task.FromResult(true);
    }

    public async Task<bool> DeleteAsync(Product? product)
    {
        if (product == null)
        {
            logger.LogWarning("Product is null");
            return false;
        }

        var existing = await context.Products.FindAsync(product.Id);
        if (existing == null)
        {
            logger.LogWarning("Product not found with id {Id}", product.Id);
            return false;
        }

        if (!await ValidateForUpdateAsync(product))
        {
            logger.LogInformation("Cannot delete product with id:{Id}, validation issues", product.Id);
            return false;
        }

        context.Products.Remove(existing);
        await context.SaveChangesAsync();
        logger.LogInformation("Product with id:{Id} deleted", existing.Id);
        return true;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        var product = await context.Products.FindAsync(id);
        if (product != null)
            logger.LogInformation("Product with id:{Id} not exists", product!.Id);
        
        logger.LogInformation("Getting product with id:{Id}", id);
        return product != null ? true : false;
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
        var productExists = product != null && await context.Products.AnyAsync(u => u.Id == product.Id);

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

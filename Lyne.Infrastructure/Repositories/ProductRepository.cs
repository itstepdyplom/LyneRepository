using System.Globalization;
using Lyne.Domain.Entities;
using Lyne.Domain.IRepositories;
using Lyne.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace Lyne.Infrastructure.Repositories;

public class ProductRepository(AppDbContext context, ILogger<AddressRepository> logger): IProductRepository
{
    public async Task<List<Product?>> GetAllAsync()
    {
        var products = await context.Products.ToListAsync();
        return (await Task.FromResult(products))!;
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        var product = await context.Products.FirstOrDefaultAsync(x => x.Id==id);
        logger.LogInformation("Product id:{Id}", product!.Id);
        return await Task.FromResult(product);
    }

    public async Task<bool> AddAsync(Product? product)
    {
        if (product == null)
        {
            logger.LogInformation("Cannot add product with id:{Id}, some fields are empty", product!.Id);
            return false;
        }
        await context.Products.AddAsync(product);
        logger.LogInformation("Product with id:{Id} added", product!.Id);
        return await Task.FromResult(true);
    }

    public async Task<bool> Update(Product? product)
    {
        if (product == null)
        {
            logger.LogInformation("Cannot update product with id:{Id}, some fields are empty", product!.Id);
            return await Task.FromResult(false);
        }
        context.Products.Update(product);
        logger.LogInformation("Product with id:{Id} updated", product!.Id);
        return await Task.FromResult(true);
    }

    public async Task<bool> DeleteAsync(Product? product)
    {
        if (! ValidateForUpdateAsync(product).Result)
        {
            logger.LogInformation("Cannot update product with id:{Id}, some fields are empty", product!.Id);
            return await Task.FromResult(false);
        }

        if (product != null)
        {
            var result = context.Products.Remove(product);
            if (result.State == EntityState.Detached)
            {
                logger.LogInformation("Product with id:{Id} deleted", product!.Id);
                return true;
            }
            else
            {
                logger.LogInformation("Cannot delete product with id:{Id}", product!.Id);
                return false;
            }
        }
        else
        {
            logger.LogInformation("Cannot delete product with id:{Id}", product!.Id);
            return false;
        }
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        var product = await context.Products.FindAsync(id);
        logger.LogInformation("Getting product with id:{Id}", id);
        return product != null ? true : false;
    }

    public async Task<bool> ValidateForCreateAsync(Product product)
    {
        return !string.IsNullOrEmpty(product.Brand) &&
               !string.IsNullOrEmpty(product.Color) &&
               !string.IsNullOrEmpty(product.Description) &&
               !string.IsNullOrEmpty(product.ImageUrl) &&
               !string.IsNullOrEmpty(product.Name) &&
               !string.IsNullOrEmpty(product.IsActive.ToString()) &&
               !string.IsNullOrEmpty(product.Price.ToString(CultureInfo.InvariantCulture)) &&
               !string.IsNullOrEmpty(product.StockQuantity.ToString());
    }

    public async Task<bool> ValidateForUpdateAsync(Product? product)
    {
        var productExists = product != null && await context.Products.AnyAsync(u => u.Id == product.Id);

        return !string.IsNullOrEmpty(product?.Brand) &&
               !string.IsNullOrEmpty(product.Color) &&
               !string.IsNullOrEmpty(product.Description) &&
               !string.IsNullOrEmpty(product.ImageUrl) &&
               !string.IsNullOrEmpty(product.Name) &&
               !string.IsNullOrEmpty(product.IsActive.ToString()) &&
               !string.IsNullOrEmpty(product.Price.ToString(CultureInfo.InvariantCulture)) &&
               !string.IsNullOrEmpty(product.StockQuantity.ToString()) &&
               productExists;
    }
}

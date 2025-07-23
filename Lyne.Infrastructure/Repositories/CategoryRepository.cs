using Lyne.Domain.Entities;
using Lyne.Domain.IRepositories;
using Lyne.Infrastructure.Caching;
using Lyne.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lyne.Infrastructure.Repositories;

public class CategoryRepository(AppDbContext context, ILogger<CategoryRepository> logger, ICacheService cacheService): ICategoryRepository
{
    public async Task<List<Category>> GetAllAsync()
    {
        var cached = await cacheService.GetAllAsync<Category>("category");
        if (cached is not null && cached.Count > 0)
            return cached;
        var categories = await context.Categories.ToListAsync();
        return categories;
    }

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        var cacheKey = $"address:{id}";
        var category = await cacheService.GetAsync<Category>(cacheKey);
        if (category is null)
        {
            category = await context.Categories.FindAsync(id);
            if (category is not null)
            {
                logger.LogInformation("Category id:{Id} retrieved", category.Id);
                await cacheService.SetAsync($"category:{id}", category, "category", TimeSpan.FromHours(1));
            }
            else
            {
                logger.LogInformation("Category with id:{Id} not found", id);
            }
        }
       
        return category;
    }

    public async Task<bool> AddAsync(Category? category)
    {
        if (category is null)
        {
            logger.LogInformation("Cannot add category with id:{Id}, some fields are empty", category!.Id);
            return false;
        }

        if (!ValidateForCreateAsync(category).Result)
        {
            logger.LogInformation("Validation error for categoryId: {Id}", category!.Id);
            return false;
        }
        var cacheKey = $"category:{category.Id}";
        await cacheService.SetAsync(cacheKey, category, "category", TimeSpan.FromHours(1));
        await context.Categories.AddAsync(category);
        logger.LogInformation("Category with id:{Id} added", category!.Id);
        return await Task.FromResult(true);
    }

    public async Task<bool> Update(Category? category)
    {
        if (category is null)
        {
            logger.LogInformation("Category with id:{Id} not found", category!.Id);
            return false;
        }

        if (!await ExistsAsync(category.Id))
        {
            logger.LogInformation("Cannot update category with id: {Id}, not found", category.Id);
            return false;
        }

        if (!ValidateForUpdateAsync(category).Result)
        {
            logger.LogInformation("Validation error for categoryId: {Id}", category!.Id);
            return false;
        }
        
        var cacheKey = $"category:{category.Id}";
        await cacheService.SetAsync(cacheKey, category, "category", TimeSpan.FromHours(1));
        context.Update(category);
        logger.LogInformation("Category with id:{Id} updated", category!.Id);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Category? category)
    {
        if (category is null)
        {
            logger.LogInformation("Cannot delete category: category is null");
            return false;
        }

        if (!await ExistsAsync(category.Id))
        {
            logger.LogInformation("Cannot delete category with id:{Id}: not found", category.Id);
            return false;
        }
        if (!await ValidateForUpdateAsync(category))
        {
            logger.LogInformation("Cannot delete category with id:{Id}, validation issues", category.Id);
            return false;
        }
        
        var cacheKey = $"category:{category.Id}";
        await cacheService.RemoveAsync(cacheKey,"category"); 
        context.Categories.Remove(category);
        await context.SaveChangesAsync();
        logger.LogInformation("Category with id:{Id} deleted", category.Id);
        return true;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        var category = await context.Categories.FindAsync(id);
        logger.LogInformation("Getting category with id:{Id}", id);
        return category != null;
    }

    public Task<bool> ValidateForCreateAsync(Category category)
    {
        bool isValid =
            !string.IsNullOrWhiteSpace(category.Name) &&
            !string.IsNullOrWhiteSpace(category.Description);
        logger.LogInformation("ValidateForCreateCategoryAsync: Validation {Result}", isValid ? "passed" : "failed");
        return Task.FromResult(isValid);
    }

    public async Task<bool> ValidateForUpdateAsync(Category? category)
    {
        if (category is null)
            return false;

        var categoryExists = await context.Categories.AnyAsync(u => u.Id == category.Id);

        bool isValid = !string.IsNullOrWhiteSpace(category.Name) &&
                       !string.IsNullOrWhiteSpace(category.Description) &&
                       categoryExists;
        logger.LogInformation("ValidateForUpdateCategoryAsync: Validation {Result}", isValid ? "passed" : "failed");
        return isValid;
    }
}

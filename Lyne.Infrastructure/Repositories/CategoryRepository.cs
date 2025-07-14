using Lyne.Domain.Entities;
using Lyne.Domain.IRepositories;
using Lyne.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lyne.Infrastructure.Repositories;

public class CategoryRepository(AppDbContext context, ILogger<CategoryRepository> logger): ICategoryRepository
{
    public async Task<List<Category>> GetAllAsync()
    {
        var categories = await context.Categories.ToListAsync();
        return categories;
    }

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        var category = await context.Categories.FindAsync(id);
        if (category != null)
            logger.LogInformation("Category id:{Id} retrieved", category.Id);
        else
            logger.LogInformation("Category with id:{Id} not found", id);
        return category;
    }

    public async Task<bool> AddAsync(Category? category)
    {
        if (category == null)
        {
            logger.LogInformation("Cannot add category with id:{Id}, some fields are empty", category!.Id);
            return false;
        }

        if (!ValidateForCreateAsync(category).Result)
        {
            logger.LogInformation("Validation error for categoryId: {Id}", category!.Id);
            return false;
        }
        
        await context.Categories.AddAsync(category);
        logger.LogInformation("Category with id:{Id} added", category!.Id);
        return await Task.FromResult(true);
    }

    public async Task<bool> Update(Category? category)
    {
        var existing = await context.Categories.FindAsync(category.Id);
        if (existing == null)
        {
            logger.LogInformation("Category with id:{Id} not found", category!.Id);
            return false;
        }

        if (!ValidateForUpdateAsync(category).Result)
        {
            logger.LogInformation("Validation error for categoryId: {Id}", category!.Id);
            return false;
        }

        existing.Name = category.Name;
        existing.Description = category.Description;
        logger.LogInformation("Category with id:{Id} updated", category!.Id);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Category? category)
    {
        if (category == null)
        {
            logger.LogInformation("Cannot delete category: category is null");
            return false;
        }

        var existing = await context.Categories.FindAsync(category.Id);
        if (existing == null)
        {
            logger.LogInformation("Cannot delete category with id:{Id}: not found", category.Id);
            return false;
        }
        if (!await ValidateForUpdateAsync(category))
        {
            logger.LogInformation("Cannot delete category with id:{Id}, validation issues", category.Id);
            return false;
        }
        context.Categories.Remove(existing);
        await context.SaveChangesAsync();
        logger.LogInformation("Category with id:{Id} deleted", category.Id);
        return true;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        var category = await context.Categories.FindAsync(id);
        logger.LogInformation("Getting category with id:{Id}", id);
        return category != null ? true : false;
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
        if (category == null)
            return false;

        var categoryExists = await context.Categories.AnyAsync(u => u.Id == category.Id);

        bool isValid = !string.IsNullOrWhiteSpace(category.Name) &&
                       !string.IsNullOrWhiteSpace(category.Description) &&
                       categoryExists;
        logger.LogInformation("ValidateForUpdateCategoryAsync: Validation {Result}", isValid ? "passed" : "failed");
        return isValid;
    }
}

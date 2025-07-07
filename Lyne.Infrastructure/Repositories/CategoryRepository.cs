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
        var category = await context.Categories.FirstOrDefaultAsync(x => x.Id==id);
        logger.LogInformation("Category id:{Id} geted", category!.Id);
        return await Task.FromResult(category);
    }

    public async Task<bool> AddAsync(Category? category)
    {
        if (category == null)
        {
            logger.LogInformation("Cannot add category with id:{Id}, some fields are empty", category!.Id);
            return false;
        }
        await context.Categories.AddAsync(category);
        logger.LogInformation("Category with id:{Id} added", category!.Id);
        return await Task.FromResult(true);
    }

    public async Task<bool> Update(Category? category)
    {
        if (category == null)
        {
            logger.LogInformation("Cannot update category with id:{Id}, some fields are empty", category!.Id);
            return await Task.FromResult(false);
        }
        context.Categories.Update(category);
        logger.LogInformation("Category with id:{Id} updated", category!.Id);
        return await Task.FromResult(true);
    }

    public async Task<bool> DeleteAsync(Category? category)
    {
        if (!ValidateForUpdateAsync(category).Result)
        {
            logger.LogInformation("Cannot update category with id:{Id}, some fields are empty", category!.Id);
            return await Task.FromResult(false);
        }
        var result = context.Categories.Remove(category);
        if (result.State == EntityState.Detached)
        {
            logger.LogInformation("Category with id:{Id} deleted", category!.Id);
            return true;
        }
        else
        {
            logger.LogInformation("Cannot delete category with id:{Id}", category!.Id);
            return false;
        }
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        var category = await context.Categories.FindAsync(id);
        logger.LogInformation("Getting category with id:{Id}", id);
        return category != null ? true : false;
    }

    public Task<bool> ValidateForCreateAsync(Category category)
    {
        return Task.FromResult(
            !string.IsNullOrWhiteSpace(category.Name) &&
            !string.IsNullOrWhiteSpace(category.Description)
        );
    }

    public async Task<bool> ValidateForUpdateAsync(Category? category)
    {
        if (category == null)
            return false;

        var categoryExists = await context.Categories.AnyAsync(u => u.Id == category.Id);

        return !string.IsNullOrWhiteSpace(category.Name) &&
               !string.IsNullOrWhiteSpace(category.Description) &&
               categoryExists;
    }
}

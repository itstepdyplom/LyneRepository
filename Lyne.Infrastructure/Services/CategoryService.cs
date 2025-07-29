using AutoMapper;
using Lyne.Application.DTO;
using Lyne.Application.Services;
using Lyne.Domain.Entities;
using Lyne.Domain.IRepositories;
using Microsoft.Extensions.Logging;

namespace Lyne.Infrastructure.Services;

public class CategoryService(ICategoryRepository categoryRepository,IMapper mapper,ILogger<CategoryService> logger):ICategoryService
{
    public async Task<List<CategoryDto>> GetAllAsync()
    {
        logger.LogInformation("Getting all categories");
        var categories = await categoryRepository.GetAllAsync();
        if (categories.Count == 0)
        {
            logger.LogInformation("Empty list");
        }
        return mapper.Map<List<CategoryDto>>(categories);
    }

    public async Task<CategoryDto?> GetByIdAsync(Guid id)
    {
        logger.LogInformation("Getting category by id");
        var category = await categoryRepository.GetByIdAsync(id);
        return mapper.Map<CategoryDto>(category);
    }

    public async Task<bool> AddAsync(CategoryDto dto)
    {
        try
        {
            var category = mapper.Map<Category>(dto);
            var result = await categoryRepository.AddAsync(category);
            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error adding category");
            return false;
        }
    }

    public async Task<bool> UpdateAsync(CategoryDto dto)
    {
        logger.LogInformation("Updating category");
        var category = mapper.Map<Category>(dto);
        return await categoryRepository.Update(category);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        logger.LogInformation("Deleting category with id: {id}",id);
        var categoryDto = await GetByIdAsync(id);
        var category = mapper.Map<Category>(categoryDto);
        return await categoryRepository.DeleteAsync(category);
    }
}

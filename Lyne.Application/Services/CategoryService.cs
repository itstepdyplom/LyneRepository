using AutoMapper;
using Lyne.Application.DTO;
using Lyne.Domain.Entities;
using Lyne.Domain.IRepositories;

namespace Lyne.Application.Services;

public class CategoryService(ICategoryRepository categoryRepository,IMapper mapper):ICategoryService
{
    public async Task<List<CategoryDto>> GetAllAsync()
    {
        var categories = await categoryRepository.GetAllAsync();
        return mapper.Map<List<CategoryDto>>(categories);
    }

    public async Task<CategoryDto?> GetByIdAsync(Guid id)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        return mapper.Map<CategoryDto>(category);
    }

    public async Task<bool> AddAsync(CategoryDto dto)
    {
        var category = mapper.Map<Category>(dto);

        if (!await categoryRepository.ValidateForCreateAsync(category))
            return false;
    
        await categoryRepository.AddAsync(category);
        return true;
    }

    public async Task<bool> UpdateAsync(CategoryDto dto)
    {
        if(dto==null)
            return false;
        if (!await categoryRepository.ExistsAsync(dto.Id))
            return false;
        
        var category = mapper.Map<Category>(dto);
        
        if (!await categoryRepository.ValidateForUpdateAsync(category))
            return false;

        await categoryRepository.Update(category);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        if (category == null) return false;

        await categoryRepository.DeleteAsync(category);
        return true;
    }
}

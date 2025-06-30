using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Lyne.Application.DTO;
using Lyne.Domain.Entities;
using Lyne.Domain.IRepositories;
using Microsoft.Extensions.Logging;

namespace Lyne.Application.Services;

public class UserService(IUserRepository userRepository,IMapper mapper,ILogger<UserService> logger)
{
    public async Task<List<UserDto>> GetAllAsync()
    {
        var users = await userRepository.GetAllAsync();
        return mapper.Map<List<UserDto>>(users);
    }

    public async Task<UserDto?> GetByIdAsync(int id)
    {
        var user = await userRepository.GetByIdAsync(id);
        return mapper.Map<UserDto>(user);
    }

    public async Task<bool> AddAsync(UserDto dto)
    {
        var validationContext = new ValidationContext(dto);
        var validationResults = new List<ValidationResult>();
        bool isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

        if (!isValid)
        {
            foreach (var error in validationResults)
            {
                logger.LogWarning("Validation failed: {ErrorMessage}", error.ErrorMessage);
            }
            return false;
        }

        var user = mapper.Map<User>(dto);
        user.CreatedAt = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;

        var result = await userRepository.AddAsync(user);
        return result;
    }

    public async Task<bool> UpdateAsync(UserDto dto)
    {
        if (!await userRepository.ExistsAsync(dto.Id))
            return false;

        var user = mapper.Map<User>(dto);
        user.UpdatedAt = DateTime.UtcNow;

        var result = userRepository.Update(user);
        return result.Result;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await userRepository.GetByIdAsync(id);
        if (user == null) return false;

        var result = userRepository.Delete(user);
        return result.Result;
    }
}

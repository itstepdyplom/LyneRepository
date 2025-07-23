using AutoMapper;
using Lyne.Application.DTO;
using Lyne.Domain.Entities;
using Lyne.Domain.IRepositories;
using Microsoft.Extensions.Logging;

namespace Lyne.Application.Services;

public class UserService(IUserRepository userRepository,IMapper mapper,ILogger<UserService> logger):IUserService
{
    public async Task<List<UserDto>> GetAllAsync()
    {
        logger.LogInformation("Getting all users");
        var users = await userRepository.GetAllAsync();
        if (users==null || users.Count == 0)
        {
            logger.LogInformation("No users found");
            return new List<UserDto>();
        }
        return mapper.Map<List<UserDto>>(users);
    }

    public async Task<UserDto?> GetByIdAsync(int id)
    {
        logger.LogInformation("Getting user with id {id}", id);
        var user = await userRepository.GetByIdAsync(id);
        return mapper.Map<UserDto>(user);
    }

    public async Task<bool> AddAsync(UserDto dto)
    {
        logger.LogInformation("Adding user {user}", dto);
        var user = mapper.Map<User>(dto);
        user.CreatedAt = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;
    
        return await userRepository.AddAsync(user);
    }

    public async Task<bool> UpdateAsync(UserDto dto)
    {
        logger.LogInformation("Updating user with id: {id}", dto.Id);
        var user = mapper.Map<User>(dto);
        user.UpdatedAt = DateTime.UtcNow;
        
        return await userRepository.Update(user);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        logger.LogInformation("Deleting user with id: {id}", id);
        var userDto = await GetByIdAsync(id);
        var user = mapper.Map<User>(userDto);

        return await userRepository.Delete(user);
    }
}

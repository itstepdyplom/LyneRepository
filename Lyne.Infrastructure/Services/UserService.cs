using AutoMapper;
using Lyne.Application.DTO;
using Lyne.Application.DTO.Auth;
using Lyne.Application.Services;
using Lyne.Domain.Entities;
using Lyne.Domain.IRepositories;
using Microsoft.Extensions.Logging;

namespace Lyne.Infrastructure.Services;

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
        user.CreatedAt = DateTimeOffset.UtcNow;
        user.UpdatedAt = DateTimeOffset.UtcNow;
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.PasswordHash);
        
        return await userRepository.AddAsync(user);
    }

    public async Task<bool> UpdateAsync(int id, UserUpdateDto dto)
    {
        var user = await userRepository.GetByIdAsync(id);
        if (user == null)
            return false;

        mapper.Map(dto, user); // <-- це оновить лише потрібні поля

        user.UpdatedAt = DateTime.UtcNow;

        return await userRepository.UpdateAsync(user);
    }

    public async Task<bool> UpdateRoleAsync(int id, string role)
    {
        var user = await userRepository.GetByIdAsync(id);
        if (user == null)
            return false;
        return await userRepository.UpdateRoleAsync(id,role);
    }
    
    public async Task<bool> DeleteAsync(int id)
    {
        logger.LogInformation("Deleting user with id: {id}", id);
        var userDto = await GetByIdAsync(id);
        var user = mapper.Map<User>(userDto);
        if (userDto != null) user.Id = userDto.Id;

        return await userRepository.DeleteAsync(user);
    } 
    public async Task<string> DeleteByIdAsync(int id)
    {
        logger.LogInformation("Deleting user with id: {id}", id);
        return await userRepository.DeleteByIdAsync(id);
    }
}

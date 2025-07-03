using AutoMapper;
using Lyne.Application.DTO;
using Lyne.Domain.Entities;
using Lyne.Domain.IRepositories;

namespace Lyne.Application.Services;

public class UserService(IUserRepository userRepository,IMapper mapper)
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
        var user = mapper.Map<User>(dto);
        user.CreatedAt = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;

        await userRepository.AddAsync(user);
    }

    public async Task<bool> UpdateAsync(UserDto dto)
    {
        if (!await userRepository.ExistsAsync(dto.Id))
            return false;
        
        var user = mapper.Map<User>(dto);
        user.UpdatedAt = DateTime.UtcNow;
        
        if (!await userRepository.ValidateForUpdateAsync(user))
            return false;

        userRepository.Update(user);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await userRepository.GetByIdAsync(id);
        if (user == null) return false;

        userRepository.Delete(user);
        return true;
    }
}

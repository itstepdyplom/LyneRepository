using System.ComponentModel.DataAnnotations;
using Lyne.Application.DTO;
using Lyne.Domain.Entities;

namespace Lyne.Application.Services;

public interface IUserService
{
    public Task<List<UserDto>> GetAllAsync();

    public Task<UserDto?> GetByIdAsync(int id);

    public Task<bool> AddAsync(UserDto dto);
    //public Task<bool> AddWithAddressAsync(UserDto dto);

    public Task<bool> UpdateAsync(UserDto dto);

    //public Task<bool> UpdateWithAddressAsync(UserDto dto);

    public Task<bool> DeleteAsync(int id);
}

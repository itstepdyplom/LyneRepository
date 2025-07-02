using Lyne.Domain.Entities;

namespace Lyne.Domain.IRepositories;

public interface IAuthRepository
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<User> CreateUserAsync(User user);
    Task<bool> UserExistsAsync(string email);
    Task<Address> CreateAddressAsync(Address address);
} 
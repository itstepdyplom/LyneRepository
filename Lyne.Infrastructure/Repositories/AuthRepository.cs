using Lyne.Domain.Entities;
using Lyne.Domain.IRepositories;
using Lyne.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Lyne.Infrastructure.Repositories;

public class AuthRepository(AppDbContext context) : IAuthRepository
{
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> CreateUserAsync(User user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> UserExistsAsync(string email)
    {
        return await context.Users
            .AnyAsync(u => u.Email == email);
    }

    public async Task<Address> CreateAddressAsync(Address address)
    {
        context.Set<Address>().Add(address);
        await context.SaveChangesAsync();
        return address;
    }
} 
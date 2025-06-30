using Lyne.Domain.Entities;
using Lyne.Domain.IRepositories;
using Lyne.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lyne.Infrastructure.Repositories;

public class UserRepository(AppDbContext context, ILogger<UserRepository> logger) : IUserRepository
{
    public async Task<List<User>> GetAllAsync()
    {
        var users = await context.Users.ToListAsync();
        logger.LogInformation("Fetched {Count} users", users.Count);
        return users;
    }
    public async Task<User?> GetByIdAsync(int id)
    {
        logger.LogInformation("User with id {UserId} not found", id);
        return await context.Users.FindAsync(id);
    }
    
    public async Task<bool> AddAsync(User? user)
    {
        if (user is null)
        {
            logger.LogWarning("Attempted to add a null user");
            return false;
        }
        var result = await context.Users.AddAsync(user);
        logger.LogInformation("User with name {UserName} added", user.Name);
        return result.State == EntityState.Added;
    }
    
    public Task<bool> Update(User? user)
    {
        if (user is null)
        {
            logger.LogWarning("Attempted to add a null user");
            return Task.FromResult(false);
        }

        var result = context.Users.Update(user);
        logger.LogInformation("User with name {UserName} updated", user.Name);
        return Task.FromResult(result.State == EntityState.Modified);
    }

    public Task<bool> Delete(User? user)
    { 
        if (user is null)
        {
            logger.LogWarning("Attempted to add a null user");
            return Task.FromResult(false);
        }

        var result = context.Users.Remove(user);
        logger.LogInformation("User with name {UserName} deleted", user.Name);
        return Task.FromResult(result.State == EntityState.Deleted);
    }
        

    public async Task<bool> ExistsAsync(int id) =>
        await context.Users.AnyAsync(u => u.Id.Equals(id));
}

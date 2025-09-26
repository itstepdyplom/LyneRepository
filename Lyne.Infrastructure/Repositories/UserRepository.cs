using Lyne.Domain.Entities;
using Lyne.Domain.IRepositories;
using Lyne.Infrastructure.Caching;
using Lyne.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lyne.Infrastructure.Repositories;

public class UserRepository(AppDbContext context, ILogger<UserRepository> logger,ICacheService cacheService) : IUserRepository
{
    public async Task<List<User>> GetAllAsync()
    {
        var cached = await cacheService.GetAllAsync<User>("user");
        if (cached is not null && cached.Count > 0)
            return cached;
        var users = await context.Users.ToListAsync();
        logger.LogInformation("Fetched {Count} users", users.Count);
        return users;
    }
    public async Task<User?> GetByIdAsync(int id)
    {
        var cacheKey = $"user:{id}";
        var user = await cacheService.GetAsync<User>(cacheKey);
        if (user is null)
        {
            user = await context.Users.FindAsync(id);
            if (user is not null)
            {
                logger.LogInformation("User with id {Id} found", user.Id);
                await cacheService.SetAsync(cacheKey, user, "user", TimeSpan.FromMinutes(15));
            }
            else
            {
                logger.LogInformation("User with id {Id} not found", id);
                return null;
            }
        }
        return user;
    }
    
    public async Task<bool> AddAsync(User? user)
    {
        if (user is null)
        {
            logger.LogWarning("Attempted to add a null user");
            return false;
        }
        if (!await ValidateForCreateAsync(user))
        {
            logger.LogInformation("Cannot add user with id:{Id}, validation issues", user.Id);
            return false;
        }

        user.Role = "User";
        var newuser = await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        var cacheKey = $"user:{newuser.Entity.Id}";
        await cacheService.SetAsync(cacheKey, user, "user", TimeSpan.FromMinutes(15));
        logger.LogInformation("User with name {UserName} added", user.Name);
        return true;
    }
    
    public async Task<bool> UpdateAsync(User? user)
    {
        if (user is null)
        {
            logger.LogWarning("Attempted to add a null user");
            return false;
        }
        
        if (!await ExistsAsync(user.Id))
        {
            logger.LogWarning("User with ID {Id} not found", user.Id);
            return false;
        }

        if (!await ValidateForUpdateAsync(user))
        {
            logger.LogInformation("Cannot update user with id:{Id}, validation issues", user.Id);
            return false;
        }

        var cacheKey = $"user:{user.Id}";
        await cacheService.SetAsync(cacheKey, user, "user", TimeSpan.FromMinutes(15));
        context.Users.Update(user);
        await context.SaveChangesAsync();
        logger.LogInformation("Product with id:{Id} updated", user!.Id);
        return true;
    }

    public async Task<bool> DeleteAsync(User? user)
    { 
        if (user is null)
        {
            logger.LogWarning("Attempted to delete null user");
            return false;
        }
        
        if (!await ExistsAsync(user.Id))
        {
            logger.LogWarning("User not found with id {Id}", user.Id);
            return false;
        }

        var cacheKey = $"user:{user.Id}";
        await cacheService.RemoveAsync(cacheKey,"user"); 
        context.Users.Remove(user);
        await context.SaveChangesAsync();
        logger.LogInformation("User with id:{Id} deleted", user.Id);
        return true;
    }
        

    public async Task<bool> ExistsAsync(int id) =>
        await context.Users.AnyAsync(u => u.Id.Equals(id));
    
    public async Task<bool> ValidateForCreateAsync(User user)
    {
        bool isValid = !string.IsNullOrEmpty(user.Name) &&
                       !string.IsNullOrEmpty(user.Email) &&
                       !string.IsNullOrEmpty(user.ForName) &&
                       !string.IsNullOrEmpty(user.Gender) &&
                       !string.IsNullOrEmpty(user.PhoneNumber);
        logger.LogInformation("ValidateForCreateUserAsync: Validation {Result}", isValid ? "passed" : "failed");
        return isValid;
    }

    public async Task<bool> ValidateForUpdateAsync(User user)
    {
        var userExists = user != null && await context.Users.AnyAsync(u => u.Id == user.Id);

        bool isValid = !string.IsNullOrEmpty(user.Name) &&
                       !string.IsNullOrEmpty(user.Email) &&
                       !string.IsNullOrEmpty(user.ForName) &&
                       !string.IsNullOrEmpty(user.Gender) &&
                       !string.IsNullOrEmpty(user.PhoneNumber) &&
                       userExists;

        logger.LogInformation("ValidateForUpdateUserAsync: Validation {Result}", isValid ? "passed" : "failed");
        return isValid;
    }
}

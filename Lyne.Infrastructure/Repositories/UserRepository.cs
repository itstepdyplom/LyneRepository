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
        var result = await context.Users.FindAsync(id);
        if (result == null)
        {
            logger.LogInformation("User with id {Id} not found", id);
            return null;
        }
        logger.LogInformation("User with id {Id} found", result.Id);
        return result;
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
        
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        logger.LogInformation("User with name {UserName} added", user.Name);
        return true;
    }
    
    public async Task<bool> Update(User? user)
    {
        if (user is null)
        {
            logger.LogWarning("Attempted to add a null user");
            return false;
        }
        
        var existing = await context.Users.FindAsync(user.Id);
        if (existing == null)
        {
            logger.LogWarning("User with ID {Id} not found", user.Id);
            return false;
        }

        if (!await ValidateForUpdateAsync(user))
        {
            logger.LogInformation("Cannot update user with id:{Id}, validation issues", user.Id);
            return false;
        }

        context.Entry(existing).CurrentValues.SetValues(user);
        await context.SaveChangesAsync();
        logger.LogInformation("Product with id:{Id} updated", user!.Id);
        return true;
    }

    public async Task<bool> Delete(User? user)
    { 
        if (user is null)
        {
            logger.LogWarning("Attempted to delete null user");
            return false;
        }
        
        var existing = await context.Users.FindAsync(user.Id);
        if (existing == null)
        {
            logger.LogWarning("User not found with id {Id}", user.Id);
            return false;
        }

        context.Users.Remove(existing);
        await context.SaveChangesAsync();
        logger.LogInformation("User with id:{Id} deleted", existing.Id);
        return true;
    }
        

    public async Task<bool> ExistsAsync(int id) =>
        await context.Users.AnyAsync(u => u.Id.Equals(id));
    
    public async Task<bool> ValidateForCreateAsync(User user)
    {
        bool isValid = !string.IsNullOrEmpty(user.Name) &&
                       !string.IsNullOrEmpty(user.Email) &&
                       !string.IsNullOrEmpty(user.ForName) &&
                       !string.IsNullOrEmpty(user.Genre) &&
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
                       !string.IsNullOrEmpty(user.Genre) &&
                       !string.IsNullOrEmpty(user.PhoneNumber) &&
                       userExists;

        logger.LogInformation("ValidateForUpdateUserAsync: Validation {Result}", isValid ? "passed" : "failed");
        return isValid;
    }
}

using Lyne.Domain.Entities;
using Lyne.Domain.IRepositories;
using Lyne.Infrastructure.Caching;
using Lyne.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lyne.Infrastructure.Repositories;

public class UserRepository(AppDbContext context, ILogger<UserRepository> logger,ICacheService cacheService) : IUserRepository
{
    private const string AllUsersKey = "users:all";
    [Authorize(Roles = "Admin")]
    public async Task<List<User>> GetAllAsync()
    {
        var cached = await cacheService.GetAsync<List<User>>(AllUsersKey);
        if (cached is not null)
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
        if (user is null) { logger.LogWarning("null user"); return false; }
        if (!await ValidateForCreateAsync(user)) { logger.LogInformation("validation failed"); return false; }
        user.CreatedAt = DateTimeOffset.UtcNow;
        user.UpdatedAt = DateTimeOffset.UtcNow;
       var response = await context.Users.AddAsync(user);

        // одна фіксація для всього
        await context.SaveChangesAsync();
        
        await cacheService.RemoveAsync(AllUsersKey, "user");
        await cacheService.SetAsync($"user:{user.Id}", user, "user", TimeSpan.FromMinutes(15));
        logger.LogInformation("User {Name} added", user.Name);
        return response.State == EntityState.Added;
        // var strategy = context.Database.CreateExecutionStrategy();
        //
        // return await strategy.ExecuteAsync(async () =>
        // {
        //     await using var tx = await context.Database.BeginTransactionAsync();
        //     try
        //     {
        //         // краще всі перевірки ДО транзакції,
        //         // але якщо треба тут — не роби early return без rollback.
        //
        //         if (user.Address is not null && user.Address.Id == 0)
        //         {
        //             await context.Addresses.AddAsync(user.Address);
        //             // Не обнуляй user.Address — EF сам підхопить FK
        //         }
        //         else if (user.AddressId.HasValue)
        //         {
        //             var exists = await context.Addresses.AnyAsync(a => a.Id == user.AddressId.Value);
        //             if (!exists)
        //             {
        //                 await tx.RollbackAsync();
        //                 logger.LogWarning("AddressId {Id} not found", user.AddressId.Value);
        //                 return false;
        //             }
        //             user.Address = null;
        //         }
        //
        //         await context.Users.AddAsync(user);
        //
        //         // одна фіксація для всього
        //         await context.SaveChangesAsync();
        //
        //         await tx.CommitAsync();
        //
        //         await cacheService.RemoveAsync(AllUsersKey, "user");
        //         await cacheService.SetAsync($"user:{user.Id}", user, "user", TimeSpan.FromMinutes(15));
        //         logger.LogInformation("User {Name} added", user.Name);
        //         return true;
        //     }
        //     catch (DbUpdateException ex) when (ex.InnerException is Npgsql.PostgresException pg)
        //     {
        //         await tx.RollbackAsync();
        //         logger.LogError(ex, "PG {Code} on {Table}: {Msg}", pg.SqlState, pg.TableName, pg.Detail ?? pg.MessageText);
        //         return false;
        //     }
        // });
    }
    
    public async Task<bool> UpdateAsync(User? user)
    {
        if (user is null)
        {
            logger.LogWarning("Attempted to add a null user");
            return false;
        }
        
        var existing = await context.Users.FirstOrDefaultAsync(a => a.Id == user.Id);
        if (existing is null) return false;
        
        if (!string.Equals(existing.Email, user.Email, StringComparison.OrdinalIgnoreCase))
        {
            var emailTaken = await context.Users
                .AnyAsync(u => u.Id != user.Id && u.Email.ToLower() == user.Email.ToLower());
            if (emailTaken)
            {
                logger.LogWarning("Email {Email} is already taken", user.Email);
                return false; 
            }
        }

        user.CreatedAt = existing.CreatedAt;
        user.UpdatedAt = DateTimeOffset.UtcNow;
        
        context.Entry(existing).CurrentValues.SetValues(user);
        await context.SaveChangesAsync();
        
        await cacheService.RemoveAsync(AllUsersKey,"user");
        var cacheKey = $"user:{user.Id}";
        await cacheService.SetAsync(cacheKey, user, "user", TimeSpan.FromMinutes(15));
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
        
        await cacheService.RemoveAsync(AllUsersKey,"user");

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

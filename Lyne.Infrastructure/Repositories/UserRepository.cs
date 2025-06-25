using Lyne.Domain.Entities;
using Lyne.Domain.IRepositories;
using Lyne.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lyne.Infrastructure.Repositories;

public class UserRepository(AppDbContext context, ILogger<UserRepository> logger) : IUserRepository
{

    public async Task<User?> GetByIdAsync(int id) =>
        await context.Users.FindAsync(id);

    public async Task<List<User>> GetAllAsync() =>
        await context.Users.ToListAsync();

    public async Task AddAsync(User user) =>
        await context.Users.AddAsync(user);

    public void Update(User user) =>
        context.Users.Update(user);

    public void Delete(User user) =>
        context.Users.Remove(user);

    public async Task<bool> ExistsAsync(int id) =>
        await context.Users.AnyAsync(u => u.Id.Equals(id));
}

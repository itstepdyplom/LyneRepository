using Lyne.Domain.Entities;

namespace Lyne.Domain.IRepositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id);
    Task<List<User>> GetAllAsync();
    Task AddAsync(User user);
    void Update(User user);
    void Delete(User user);
    Task<bool> ExistsAsync(int id);
    public Task<bool> ValidateForCreateAsync(User user);
    public Task<bool> ValidateForUpdateAsync(User user);
}

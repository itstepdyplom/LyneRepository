using Lyne.Domain.Entities;

namespace Lyne.Domain.IRepositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id);
    Task<List<User>> GetAllAsync();
    Task<bool> AddAsync(User user);
    Task<bool> Update(User? user);
    Task<bool> Delete(User? user);
    Task<bool> ExistsAsync(int id);
}

using Lyne.Domain.Entities;

namespace Lyne.Domain.IRepositories;

public interface IAddressRepository
{
    Task<List<Address>> GetAllAsync();
    Task<Address?> GetByIdAsync(int id);
    Task<bool> AddAsync(Address address);
    Task<bool> Update(Address? address);
    Task<bool> DeleteAsync(Address address);
    Task<bool> ExistsAsync(int id);
    Task<bool> ValidateForCreateAsync(Address address);
    Task<bool> ValidateForUpdateAsync(Address address);
}

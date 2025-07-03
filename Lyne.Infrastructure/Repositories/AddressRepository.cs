using Lyne.Domain.Entities;
using Lyne.Domain.IRepositories;
using Lyne.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lyne.Infrastructure.Repositories;

public class AddressRepository(AppDbContext context, ILogger<AddressRepository> logger): IAddressRepository
{
    public async Task<Address?> GetByIdAsync(int id)
    {
        var address = await context.Addresses.FirstOrDefaultAsync(x => x.Id == id);
        logger.LogInformation("Address id:{Id} users", address!.Id);
        return await Task.FromResult(address);
    }

    public async Task<bool> AddAsync(Address address)
    {
        if (address?.User == null)
        {
            logger.LogInformation("Cannot add address with id:{Id}, some fields are empty", address!.Id);
            return false;
        }
        await context.Addresses.AddAsync(address);
        logger.LogInformation("Address with id:{Id} added", address!.Id);
        return await Task.FromResult(true);
    }

    public async Task<bool> Update(Address? address)
    {
        if (address?.User == null)
        {
            logger.LogInformation("Cannot update address with id:{Id}, some fields are empty", address!.Id);
            return await Task.FromResult(false);
        }
        context.Addresses.Update(address);
        logger.LogInformation("Address with id:{Id} updated", address!.Id);
        return await Task.FromResult(true);
    }

    public async Task<bool> DeleteAsync(Address address)
    {
        if (! ValidateForUpdateAsync(address).Result)
        {
            logger.LogInformation("Cannot update address with id:{Id}, some fields are empty", address!.Id);
            return await Task.FromResult(false);
        }
        var result = context.Addresses.Remove(address);
        if (result.State == EntityState.Detached)
        {
            logger.LogInformation("Address with id:{Id} deleted", address!.Id);
            return true;
        }
        else
        {
            logger.LogInformation("Cannot delete address with id:{Id} deleted", address!.Id);
            return false;
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        var address = await context.Addresses.FindAsync(id);
        logger.LogInformation("Getting address with id:{Id}", id);
        return address != null ? true : false;
    }

    public async Task<bool> ValidateForCreateAsync(Address address)
    {
        return !string.IsNullOrEmpty(address.City) &&
               !string.IsNullOrEmpty(address.State) &&
               !string.IsNullOrEmpty(address.Country) &&
               !string.IsNullOrEmpty(address.Street) &&
               !string.IsNullOrEmpty(address.Zip);
    }

    public async Task<bool> ValidateForUpdateAsync(Address address)
    {
        var userExists = address.User != null && await context.Users.AnyAsync(u => u.Id == address.User.Id);

        return !string.IsNullOrEmpty(address.City) &&
               !string.IsNullOrEmpty(address.State) &&
               !string.IsNullOrEmpty(address.Country) &&
               !string.IsNullOrEmpty(address.Street) &&
               !string.IsNullOrEmpty(address.Zip) &&
               userExists;
    }
}

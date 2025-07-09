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
        if (address != null)
            logger.LogInformation("Address id:{Id} users", address!.Id);
        else
        {
            logger.LogInformation("No address found for id:{Id}", id);
            return null;
        }
        return await Task.FromResult(address);
    }

    public async Task<bool> AddAsync(Address address)
    {
        if (address == null || address.UserId == 0)
        {
            logger.LogInformation("Cannot add address, UserId is missing or address is null");
            return false;
        }
        await context.Addresses.AddAsync(address);
        await context.SaveChangesAsync();
        logger.LogInformation("Address with id:{Id} added", address.Id);
        return true;
    }

    public async Task<bool> Update(Address? address)
    {
        if (address?.User == null)
        {
            logger.LogInformation("Cannot update address with id:{Id}, some fields are empty", address!.Id);
            return false;
        }
        context.Addresses.Update(address);
        logger.LogInformation("Address with id:{Id} updated", address!.Id);
        return true;
    }

    public async Task<bool> DeleteAsync(Address address)
    {
        var isValid = await ValidateForUpdateAsync(address);
        if (!isValid)
        {
            logger.LogInformation("Cannot delete address with id: {Id}, validation failed", address.Id);
            return false;
        }

        context.Addresses.Remove(address);
        await context.SaveChangesAsync(); // треба, щоб фактично видалити з БД

        logger.LogInformation("Address with id: {Id} deleted", address.Id);
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        var address = await context.Addresses.FindAsync(id);
        logger.LogInformation("Getting address with id:{Id}", id);
        return address != null;
    }

    public async Task<bool> ValidateForCreateAsync(Address address)
    {
        if (address == null)
            return false;

        if (address.UserId == 0) // або якщо UserId nullable - null
            return false;

        var userExists = await context.Users.AnyAsync(u => u.Id == address.UserId);
        if (!userExists)
            return false;

        if (string.IsNullOrWhiteSpace(address.City) ||
            string.IsNullOrWhiteSpace(address.Country) ||
            string.IsNullOrWhiteSpace(address.Street) ||
            string.IsNullOrWhiteSpace(address.State) ||
            string.IsNullOrWhiteSpace(address.Zip))
            return false;

        return true;
    }

    public async Task<bool> ValidateForUpdateAsync(Address address)
    {
        if (address.UserId == null ||
            string.IsNullOrWhiteSpace(address.City) ||
            string.IsNullOrWhiteSpace(address.State) ||
            string.IsNullOrWhiteSpace(address.Country) ||
            string.IsNullOrWhiteSpace(address.Street) ||
            string.IsNullOrWhiteSpace(address.Zip))
        {
            return false;
        }

        var userExists = await context.Users.AnyAsync(u => u.Id == address.UserId);
        return userExists;
    }
}

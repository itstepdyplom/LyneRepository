using Lyne.Domain.Entities;
using Lyne.Domain.IRepositories;
using Lyne.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lyne.Infrastructure.Repositories;

public class AddressRepository(AppDbContext context, ILogger<AddressRepository> logger): IAddressRepository
{
    public async Task<List<Address>> GetAllAsync()
    {
        var addresses = await context.Addresses.ToListAsync();
        return addresses;
    }
    public async Task<Address?> GetByIdAsync(int id)
    {
        var address = await context.Addresses.FirstOrDefaultAsync(x => x.Id == id);
        if (address != null)
            logger.LogInformation("Address with id:{Id} was found", address!.Id);
        else
        {
            logger.LogInformation("Address with id: {id} was not found", id);
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
        var isValid = await ValidateForCreateAsync(address);
        if (!isValid)
        {
            logger.LogInformation("Cannot add address with id: {Id}, validation failed", address.Id);
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

        var exist = ExistsAsync(address.Id);
        if (!exist.Result)
        {
            logger.LogInformation("Cannot update address with id: {Id}, not found", address.Id);
            return false;
        }
        var isValid = await ValidateForUpdateAsync(address);
        if (!isValid)
        {
            logger.LogInformation("Cannot update address with id: {Id}, validation failed", address.Id);
            return false;
        }
        context.Addresses.Update(address);
        logger.LogInformation("Address with id:{Id} updated", address!.Id);
        return true;
    }

    public async Task<bool> DeleteAsync(Address address)
    {
        var exist = await ExistsAsync(address.Id);
        if (!exist)
        {
            logger.LogInformation("Cannot delete address with id: {Id}, not found", address.Id);
            return false;
        }
        
        var isValid = await ValidateForUpdateAsync(address);
        if (!isValid)
        {
            logger.LogInformation("Cannot delete address with id: {Id}, validation failed", address.Id);
            return false;
        }

        context.Addresses.Remove(address);
        await context.SaveChangesAsync();

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

        if (address.UserId == 0)
            return false;

        var userExists = await context.Users.AnyAsync(u => u.Id == address.UserId);
        if (!userExists)
            return false;

        bool isValid = !string.IsNullOrWhiteSpace(address.City) &&
                       !string.IsNullOrWhiteSpace(address.Country) &&
                       !string.IsNullOrWhiteSpace(address.Street) &&
                       !string.IsNullOrWhiteSpace(address.State) &&
                       !string.IsNullOrWhiteSpace(address.Zip);

        logger.LogInformation("ValidateForCreateAddressAsync: Validation {Result}", isValid ? "passed" : "failed");
        return isValid;
    }

    public async Task<bool> ValidateForUpdateAsync(Address address)
    {
        var userExists = await context.Users.AnyAsync(u => u.Id == address.UserId);

        bool isValid = address.UserId != null &&
                       !string.IsNullOrWhiteSpace(address.City) &&
                       !string.IsNullOrWhiteSpace(address.State) &&
                       !string.IsNullOrWhiteSpace(address.Country) &&
                       !string.IsNullOrWhiteSpace(address.Street) &&
                       !string.IsNullOrWhiteSpace(address.Zip) &&
                       userExists;

        logger.LogInformation("ValidateForUpdateAddressAsync: Validation {Result}", isValid ? "passed" : "failed");
        return isValid;
    }
}

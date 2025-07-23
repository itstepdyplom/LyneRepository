using Lyne.Domain.Entities;
using Lyne.Domain.IRepositories;
using Lyne.Infrastructure.Caching;
using Lyne.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lyne.Infrastructure.Repositories;

public class AddressRepository(AppDbContext context, ILogger<AddressRepository> logger, ICacheService cacheService): IAddressRepository
{
    public async Task<List<Address?>> GetAllAsync()
    {
        var cached = await cacheService.GetAllAsync<Address>("address");
        if (cached is not null && cached.Count > 0)
            return cached;
        var addresses = await context.Addresses.ToListAsync();
        return addresses!;
    }
    public async Task<Address?> GetByIdAsync(int id)
    {
        var cacheKey = $"address:{id}";
        var address = await cacheService.GetAsync<Address>(cacheKey);
        if (address is null)
        {
            address = await context.Addresses.FirstOrDefaultAsync(x => x.Id == id);
            if (address is not null)
            {
                logger.LogInformation("Address with id:{Id} was found", address!.Id);
                await cacheService.SetAsync(cacheKey, address, "address", TimeSpan.FromMinutes(15));
            }
            else
            {
                logger.LogInformation("Address with id: {id} was not found", id);
                return null;
            }
        }
        return address;
    }

    public async Task<bool> AddAsync(Address? address)
    {
        if (address is null)
        {
            logger.LogInformation("Cannot add address, UserId is missing or address is null");
            return false;
        }
        if (!await ValidateForCreateAsync(address))
        {
            logger.LogInformation("Cannot add address with id: {Id}, validation failed", address.Id);
            return false;
        }
        var cacheKey = $"address:{address.Id}";
        await cacheService.SetAsync(cacheKey, address, "address", TimeSpan.FromMinutes(15));
        await context.Addresses.AddAsync(address);
        await context.SaveChangesAsync();
        logger.LogInformation("Address with id:{Id} added", address.Id);
        return true;
    }

    public async Task<bool> Update(Address? address)
    {
        if (address is null)
        {
            logger.LogInformation("Cannot update address with id:{Id}, some fields are empty", address!.Id);
            return false;
        }

        if (!await ExistsAsync(address.Id))
        {
            logger.LogInformation("Cannot update address with id: {Id}, not found", address.Id);
            return false;
        }
        if (!await ValidateForUpdateAsync(address))
        {
            logger.LogInformation("Cannot update address with id: {Id}, validation failed", address.Id);
            return false;
        }
        var cacheKey = $"address:{address.Id}";
        await cacheService.SetAsync(cacheKey, address, "address", TimeSpan.FromMinutes(15));
        context.Addresses.Update(address);
        await context.SaveChangesAsync();
        logger.LogInformation("Address with id:{Id} updated", address!.Id);
        return true;
    }

    public async Task<bool> DeleteAsync(Address address)
    {
        if (address is null)
        {
            logger.LogInformation("Cannot delete address: address is null");
            return false;
        }
        
        if (!await ExistsAsync(address.Id))
        {
            logger.LogInformation("Cannot delete address with id: {Id}, not found", address.Id);
            return false;
        }
        
        if (!await ValidateForUpdateAsync(address))
        {
            logger.LogInformation("Cannot delete address with id: {Id}, validation failed", address.Id);
            return false;
        }

        var cacheKey = $"address:{address.Id}";
        await cacheService.RemoveAsync(cacheKey,"address"); 
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
        if (address is null)
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
        bool isValid = !string.IsNullOrWhiteSpace(address.City) &&
                       !string.IsNullOrWhiteSpace(address.State) &&
                       !string.IsNullOrWhiteSpace(address.Country) &&
                       !string.IsNullOrWhiteSpace(address.Street) &&
                       !string.IsNullOrWhiteSpace(address.Zip);

        logger.LogInformation("ValidateForUpdateAddressAsync: Validation {Result}", isValid ? "passed" : "failed");
        return isValid;
    }
}

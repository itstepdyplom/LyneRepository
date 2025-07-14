using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Lyne.Application.DTO;
using Lyne.Domain.Entities;
using Lyne.Domain.IRepositories;
using Microsoft.Extensions.Logging;

namespace Lyne.Application.Services;

public class AddressService(IAddressRepository addressRepository, IMapper mapper, ILogger<AddressService> logger):IAddressService
{
    public async Task<List<AddressDto>> GetAllAsync()
    {
        logger.LogInformation("Getting all addresses");
        var addresses = await addressRepository.GetAllAsync();
        if (addresses.Count == 0)
        {
            logger.LogInformation("No addresses found");
        }
        return mapper.Map<List<AddressDto>>(addresses);
    }
    public async Task<AddressDto?> GetByIdAsync(int id)
    {
        logger.LogInformation("Getting address with id: {id}", id);
        var address = await addressRepository.GetByIdAsync(id);
        return mapper.Map<AddressDto>(address);
    }

    public async Task<bool> AddAsync(AddressDto dto)
    {
        logger.LogInformation("Adding address with id: {id}", dto.Id);
        var address = mapper.Map<Address>(dto);
        if (address == null || string.IsNullOrWhiteSpace(address.City) || address.User == null || address.User.Id == 0)
        {
            logger.LogInformation("Cannot add address, validation failed");
            return false;
        }
        return await addressRepository.AddAsync(address);
    }

    public async Task<bool> UpdateAsync(AddressDto dto)
    {
        logger.LogInformation("Updating address with id: {id}", dto.Id);
        var address = mapper.Map<Address>(dto);
        if (address == null)
        {
            logger.LogInformation("Cannot update address, address is null");
            return false;
        }
        return await addressRepository.Update(address);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        logger.LogInformation("Deleting address with id: {id}", id);
        var addressDto = await GetByIdAsync(id);
        var address = mapper.Map<Address>(addressDto);
        if (address == null)
        {
            logger.LogInformation("Cannot delete address, address is null");
            return false;
        }
        return await addressRepository.DeleteAsync(address);
    }
}

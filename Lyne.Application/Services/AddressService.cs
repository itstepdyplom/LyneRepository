using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Lyne.Application.DTO;
using Lyne.Domain.Entities;
using Lyne.Domain.IRepositories;
using Microsoft.Extensions.Logging;

namespace Lyne.Application.Services;

public class AddressService(IAddressRepository addressRepository, IMapper mapper):IAddressService
{
    public async Task<AddressDto?> GetByIdAsync(int id)
    {
        var validation = await addressRepository.ExistsAsync(id);
        if (!validation) return null;
        var address = await addressRepository.GetByIdAsync(id);
        return mapper.Map<AddressDto>(address);
    }

    public async Task<bool> AddAsync(AddressDto dto)
    {
        var address = mapper.Map<Address>(dto);

        var validation = await addressRepository.ValidateForCreateAsync(address);
        if (!validation) return false;

        return await addressRepository.AddAsync(address);
    }

    public async Task<bool> UpdateAsync(AddressDto dto)
    {
        var address = mapper.Map<Address>(dto);
        
        if (!await addressRepository.ExistsAsync(address.Id)) return false;
        
        var validation = await addressRepository.ValidateForUpdateAsync(address);
        if (!validation) return false;

        return await addressRepository.Update(address);
    }

    public async Task<bool> DeleteAsync(AddressDto dto)
    {
        var address = mapper.Map<Address>(dto);

        if (!await addressRepository.ExistsAsync(address.Id)) return false;

        if (!await addressRepository.ValidateForUpdateAsync(address)) return false;

        return await addressRepository.DeleteAsync(address);
    }
}

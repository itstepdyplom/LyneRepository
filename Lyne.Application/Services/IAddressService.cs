using Lyne.Application.DTO;
using Lyne.Domain.Entities;

namespace Lyne.Application.Services;

public interface IAddressService
{
    public Task<AddressDto?> GetByIdAsync(int id);

    public Task<bool> AddAsync(AddressDto dto);

    public Task<bool> UpdateAsync(AddressDto dto);

    public Task<bool> DeleteAsync(AddressDto dto);
}

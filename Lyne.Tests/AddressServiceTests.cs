using AutoMapper;
using FluentAssertions;
using Lyne.Application.DTO;
using Lyne.Application.Services;
using Lyne.Domain.Entities;
using Lyne.Domain.IRepositories;
using Microsoft.Extensions.Logging;
using Moq;

namespace Lyne.Tests;

public class AddressServiceTests
{
     private readonly Mock<IAddressRepository> _addressRepoMock;
    private readonly AddressService _service;
    private readonly IMapper _mapper;

    public AddressServiceTests()
    {
        _addressRepoMock = new Mock<IAddressRepository>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<Lyne.Application.Mapping.MappingProfile>();
        });
        _mapper = config.CreateMapper();


        _service = new AddressService(_addressRepoMock.Object, _mapper);
    }
    
    [Fact]
    public async Task GetAsync_ReturnsAddress_WhenAddressExists()
    {
        // Arrange
        var addressId = 1;
        var user = new User()
        {
            Id = addressId,
            AddressId = 1,
            Name = "John",
            ForName = "Doe",
        };
        var expectedAddress = new Address { Id = addressId, City = "Test",Country = "Test",State = "Test",Street = "Test",User = user,Zip = "Test"};
        _addressRepoMock.Setup(r => r.GetByIdAsync(addressId)).ReturnsAsync(expectedAddress);
        _addressRepoMock.Setup(r => r.ExistsAsync(user.Id)).ReturnsAsync(true);

        // Act
        var result = await _service.GetByIdAsync(addressId);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(addressId);
    }
    
    [Fact]
    public async Task GetAsync_ReturnsNull_WhenAddressNotExists()
    {
        // Arrange
        int addressId = 99;
       
        _addressRepoMock.Setup(r => r.GetByIdAsync(addressId)).ReturnsAsync((Address?)null);
        
        // Act
        var result = await _service.GetByIdAsync(addressId);

        // Assert
        result.Should().BeNull();
    }
    
    [Fact]
    public async Task AddAsync_ReturnsTrue_WhenAddressAdded()
    {
        // Arrange
        int addressId = 1;
        var user = new User()
        {
            Id = 1,
            AddressId = addressId,
            Name = "John",
            ForName = "Doe",
        };
        var address = new Address()
        {
            Id = addressId,
            City = "Test",
            Country = "Test",
            State = "Test",
            Street = "Test",
            User = user,
            Zip = "Test"
        };
        _addressRepoMock.Setup(r => r.ExistsAsync(addressId)).ReturnsAsync(true);
        _addressRepoMock.Setup(r => r.ValidateAsync(It.IsAny<Address>())).ReturnsAsync(true);
        _addressRepoMock.Setup(r => r.AddAsync(It.IsAny<Address>())).ReturnsAsync(true);

        // Act
        var addressDto = _mapper.Map<AddressDto>(address);
        var result = await _service.AddAsync(addressDto);

        // Assert
        result.Should().BeTrue();
    }
    
    [Fact]
    public async Task AddAsync_ReturnsFalse_WhenThereIsValidationError()
    {
        // Arrange
        int addressId = 1;
        var user = new User()
        {
            Id = 1,
            AddressId = addressId,
            Name = "John",
            ForName = "Doe",
        };
        var address = new Address()
        {
            Id = addressId,
            City = null,
            Country = "Test",
            State = "Test",
            Street = "Test",
            User = user,
            Zip = "Test"
        };
        _addressRepoMock.Setup(r => r.AddAsync(It.IsAny<Address>())).ReturnsAsync(false);

        // Act
        var addressDto = _mapper.Map<AddressDto>(address);
        var result = await _service.AddAsync(addressDto);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public async Task UpdateAsync_ReturnsTrue_WhenAddressUpdated()
    {
        // Arrange
        int addressId = 1;
        var user = new User()
        {
            Id = 1,
            AddressId = addressId,
            Name = "John",
            ForName = "Doe",
        };
        var address = new Address()
        {
            Id = addressId,
            City = "Test",
            Country = "Test",
            State = "Test",
            Street = "Test",
            User = user,
            Zip = "Test"
        };
        _addressRepoMock.Setup(r => r.Update(It.IsAny<Address>())).ReturnsAsync(true);
        _addressRepoMock.Setup(r => r.ExistsAsync(address.Id)).ReturnsAsync(true);
        _addressRepoMock.Setup(r => r.ValidateAsync(It.IsAny<Address>())).ReturnsAsync(true);

        // Act
        var addressDto = _mapper.Map<AddressDto>(address);
        var result = await _service.UpdateAsync(addressDto);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateAsync_ReturnsFalse_WhenThereIsValidationError()
    {
        // Arrange
        int addressId = 1;
        var user = new User()
        {
            Id = 1,
            AddressId = addressId,
            Name = "John",
            ForName = "Doe",
        };
        var address = new Address()
        {
            Id = addressId,
            City = "Test",
            Country = "Test",
            State = "Test",
            Street = "Test",
            User = user,
            Zip = "Test"
        };
        _addressRepoMock.Setup(r => r.Update(It.IsAny<Address>())).ReturnsAsync(true);
        _addressRepoMock.Setup(r => r.ValidateAsync(It.IsAny<Address>())).ReturnsAsync(false);
        _addressRepoMock.Setup(r => r.ExistsAsync(user.Id)).ReturnsAsync(true);

        // Act
        var addressDto = _mapper.Map<AddressDto>(address);
        var result = await _service.UpdateAsync(addressDto);

        // Assert
        result.Should().BeFalse();
    }
    [Fact]
    public async Task UpdateAsync_ReturnsFalse_WhenAddressNotExists()
    {
        // Arrange
        int addressId = 1;
        var user = new User()
        {
            Id = 1,
            AddressId = addressId,
            Name = "John",
            ForName = "Doe",
        };
        var address = new Address()
        {
            Id = addressId,
            City = "Test",
            Country = "Test",
            State = "Test",
            Street = "Test",
            User = user,
            Zip = "Test"
        };
        _addressRepoMock.Setup(r => r.Update(It.IsAny<Address>())).ReturnsAsync(true);
        _addressRepoMock.Setup(r => r.ValidateAsync(It.IsAny<Address>())).ReturnsAsync(true);
        _addressRepoMock.Setup(r => r.ExistsAsync(user.Id)).ReturnsAsync(false);

        // Act
        var addressDto = _mapper.Map<AddressDto>(address);
        var result = await _service.UpdateAsync(addressDto);
        // Assert
        result.Should().BeFalse();
    }

    [Fact] 
    public async Task DeleteAsync_ReturnsTrue_WhenAddressDeleted()
    {
        // Arrange
        int addressId = 1;
        var user = new User()
        {
            Id = 1,
            AddressId = addressId,
            Name = "John",
            ForName = "Doe",
        };
        var address = new Address()
        {
            Id = addressId,
            City = "Test",
            Country = "Test",
            State = "Test",
            Street = "Test",
            User = user,
            Zip = "Test"
        };
        _addressRepoMock.Setup(r => r.ExistsAsync(user.Id)).ReturnsAsync(true);
        _addressRepoMock.Setup(r => r.ValidateAsync(It.IsAny<Address>())).ReturnsAsync(true);
        _addressRepoMock.Setup(r => r.DeleteAsync(It.IsAny<Address>())).ReturnsAsync(true);

        // Act
        var addressDto = _mapper.Map<AddressDto>(address);
        var result = await _service.DeleteAsync(addressDto);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteAsync_ReturnsFalse_WhenThereIsValidationError()
    {
        // Arrange
        int addressId = 1;
        var user = new User()
        {
            Id = 1,
            AddressId = addressId,
            Name = "John",
            ForName = "Doe",
        };
        var address = new Address()
        {
            Id = addressId,
            City = "Test",
            Country = "Test",
            State = "Test",
            Street = "Test",
            User = user,
            Zip = "Test"
        };
        _addressRepoMock.Setup(r => r.DeleteAsync(It.IsAny<Address>())).ReturnsAsync(true);
        _addressRepoMock.Setup(r => r.ExistsAsync(address.Id)).ReturnsAsync(true);
        _addressRepoMock.Setup(r => r.ValidateAsync(address)).ReturnsAsync(false);

        // Act
        var addressDto = _mapper.Map<AddressDto>(address);
        var result = await _service.DeleteAsync(addressDto);

        // Assert
        result.Should().BeFalse();
    }
    [Fact]
    public async Task DeleteAsync_ReturnsFalse_WhenAddressNotExists()
    {
        // Arrange
        int addressId = 1;
        var user = new User()
        {
            Id = 1,
            AddressId = addressId,
            Name = "John",
            ForName = "Doe",
        };
        var address = new Address()
        {
            Id = addressId,
            City = "Test",
            Country = "Test",
            State = "Test",
            Street = "Test",
            User = user,
            Zip = "Test"
        };
        _addressRepoMock.Setup(r => r.DeleteAsync(It.IsAny<Address>())).ReturnsAsync(true);
        _addressRepoMock.Setup(r => r.ExistsAsync(address.Id)).ReturnsAsync(false);
        _addressRepoMock.Setup(r => r.ValidateAsync(address)).ReturnsAsync(true);

        // Act
        var addressDto = _mapper.Map<AddressDto>(address);
        var result = await _service.DeleteAsync(addressDto);

        // Assert
        result.Should().BeFalse();
    }
}

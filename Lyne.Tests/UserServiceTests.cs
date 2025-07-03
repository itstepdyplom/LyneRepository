using AutoMapper;
using FluentAssertions;
using Lyne.Application.DTO;
using Lyne.Application.Services;
using Lyne.Domain.Entities;
using Lyne.Domain.IRepositories;
using Microsoft.Extensions.Logging;
using Moq;

namespace Lyne.Tests;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepoMock;
    private readonly UserService _service;
    private readonly IMapper _mapper;

    public UserServiceTests()
    {
        _userRepoMock = new Mock<IUserRepository>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<Lyne.Application.Mapping.MappingProfile>();
        });
        _mapper = config.CreateMapper();


        _service = new UserService(_userRepoMock.Object, _mapper);
    }
    [Fact]
    public async Task GetAllAsync_ReturnsListOfUsers_WhenUsersExists()
    {
        // Arrange
        _userRepoMock.Setup(r => r.GetAllAsync())
            .ReturnsAsync(new List<User> { new User { Id = 1, Name = "Test", ForName = "User" }, new User { Id = 2, Name = "Test2", ForName = "User2" } });

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        result!.Should().NotBeNullOrEmpty();
    }
    [Fact]
    public async Task GetAllAsync_ReturnsNull_WhenUsersDoesNotExist()
    {
        // Arrange
        var id = new int();
        _userRepoMock.Setup(r => r.GetAllAsync())!.ReturnsAsync((List<User>)null);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        result.Should().BeEmpty();
    }
    [Fact]
    public async Task GetByIdAsync_ReturnsUserDto_WhenUserExists()
    {
        // Arrange
        var id = new int();
        _userRepoMock.Setup(r => r.GetByIdAsync(id))
            .ReturnsAsync(new User { Id = 1, Name = "Test", ForName = "User" });

        // Act
        var result = await _service.GetByIdAsync(id);

        // Assert
        result.Should().NotBeNull();
        result!.Name.Should().Be("Test");
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenUserDoesNotExist()
    {
        // Arrange
        var id = new int();
        _userRepoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((User?)null);

        // Act
        var result = await _service.GetByIdAsync(id);

        // Assert
        result.Should().BeNull();
    }
    [Fact]
    public async Task AddAsync_ReturnsTrue_WhenUserCreated()
    {
        // Arrange
        var address = new AddressDto()
        {
            City = "City",
            Country = "Country",
            State = "State",
            Street = "Street",
            Id = 1,
            Zip = "Zip",
            UserId = 1
        };
        var userDto = new UserDto
        {
            Id = 1, 
            Name = "Test",
            ForName = "User",
            Address = address, 
            DateOfBirth = new DateTime(2000, 1, 1),
            Email = "test@gmail.com",
            Genre = "Male",
            OrderIds = new List<int> { 1 },
            PasswordHash = "pass",
            PhoneNumber = "3809877777777",
        };
        
        _userRepoMock.Setup(r => r.AddAsync(It.Is<User>(u =>
            u.Id == userDto.Id &&
            u.Name == userDto.Name &&
            u.ForName == userDto.ForName &&
            u.Email == userDto.Email &&
            u.Genre == userDto.Genre &&
            u.PhoneNumber == userDto.PhoneNumber &&
            u.DateOfBirth == userDto.DateOfBirth &&
            u.CreatedAt.Date == DateTime.UtcNow.Date &&
            u.UpdatedAt.Date == DateTime.UtcNow.Date
            ))).ReturnsAsync(true);

        // Act
        var result = await _service.AddAsync(userDto);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task AddAsync_ReturnsFalse_WhenUserNameIsEmpty()
    {
        // Arrange
        var address = new AddressDto()
        {
            City = "City",
            Country = "Country",
            State = "State",
            Street = "Street",
            Id = 1,
            Zip = "Zip",
            UserId = 1
        };
        var userDto = new UserDto
        {
            Id = 1, 
            Name = null,
            ForName = "User",
            Address = address, 
            DateOfBirth = new DateTime(2000, 1, 1),
            Email = "test@gmail.com",
            Genre = "Male",
            OrderIds = new List<int> { 1 },
            PasswordHash = "pass",
            PhoneNumber = "3809877777777",
        };

        // Act
        var result = await _service.AddAsync(userDto);

        // Assert
        result.Should().BeFalse();
    }
   
    [Fact]
    public async Task UpdateAsync_ReturnsTrue_WhenUserIsUpdated()
    {
        // Arrange
        var address = new AddressDto()
        {
            City = "City",
            Country = "Country",
            State = "State",
            Street = "Street",
            Id = 1,
            Zip = "Zip",
            UserId = 1
        };
        var userDto = new UserDto
        {
            Id = 1, 
            Name = "Test",
            ForName = "User",
            Address = address, 
            DateOfBirth = new DateTime(2000, 1, 1),
            Email = "test@gmail.com",
            Genre = "Male",
            OrderIds = new List<int> { 1 },
            PasswordHash = "pass",
            PhoneNumber = "3809877777777",
        };
        _userRepoMock.Setup(r => r.ExistsAsync(userDto.Id)).ReturnsAsync(true);
        _userRepoMock.Setup(r => r.Update(It.IsAny<User>())).ReturnsAsync(true);

        // Act
        var result = await _service.UpdateAsync(userDto);

        // Assert
        result.Should().BeTrue();
    }
    
    [Fact]
    public async Task UpdateAsync_ReturnsFalse_WhenUserNotExists()
    {
        // Arrange
        
        var userDto = new UserDto
        {
            Id = 1, 
            Name = "Test",
            ForName = "User",
            DateOfBirth = new DateTime(2000, 1, 1),
            Genre = "Male",
            OrderIds = new List<int> { 1 },
            PasswordHash = "pass",
            PhoneNumber = "3809877777777",
        };
        _userRepoMock.Setup(r => r.ExistsAsync(userDto.Id)).ReturnsAsync(false);

        // Act
        var result = await _service.UpdateAsync(userDto);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public async Task UpdateAsync_ReturnsFalse_WhenThereIsValidationError()
    {
        // Arrange
        
        var userDto = new UserDto
        {
            Id = 1, 
            Name = "Test",
            ForName = "User",
            DateOfBirth = new DateTime(2000, 1, 1),
            Genre = "Male",
            OrderIds = new List<int> { 1 },
            PasswordHash = "pass",
            PhoneNumber = "3809877777777",
        };
        _userRepoMock.Setup(r => r.ExistsAsync(userDto.Id)).ReturnsAsync(false);

        // Act
        var result = await _service.UpdateAsync(userDto);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public async Task DeleteAsync_ReturnsTrue_WhenUserIsDeleted()
    {
        // Arrange
        var address = new AddressDto()
        {
            City = "City",
            Country = "Country",
            State = "State",
            Street = "Street",
            Id = 1,
            Zip = "Zip",
            UserId = 1
        };
        var userDto = new UserDto
        {
            Id = 1, 
            Name = "Test",
            ForName = "User",
            Address = address, 
            DateOfBirth = new DateTime(2000, 1, 1),
            Email = "test@gmail.com",
            Genre = "Male",
            OrderIds = new List<int> { 1 },
            PasswordHash = "pass",
            PhoneNumber = "3809877777777",
        };
        var user= await _service.GetByIdAsync(userDto.Id);
        _userRepoMock.Setup(r => r.GetByIdAsync(userDto.Id)).ReturnsAsync(new User { Id = userDto.Id,Name = userDto.Name,ForName = userDto.ForName});
        _userRepoMock.Setup(r => r.Delete(It.IsAny<User>())).ReturnsAsync(true);

        // Act
        var result = await _service.DeleteAsync(userDto.Id);

        // Assert
        result.Should().BeTrue();
    }
    
    [Fact]
    public async Task DeleteAsync_ReturnsFalse_WhenUserNotExists()
    {
        // Arrange
        
        var userDto = new UserDto
        {
            Id = 1, 
            Name = "Test",
            ForName = "User",
            DateOfBirth = new DateTime(2000, 1, 1),
            Genre = "Male",
            OrderIds = new List<int> { 1 },
            PasswordHash = "pass",
            PhoneNumber = "3809877777777",
        };
        _userRepoMock.Setup(r => r.ExistsAsync(userDto.Id)).ReturnsAsync(false);

        // Act
        var result = await _service.DeleteAsync(userDto.Id);

        // Assert
        result.Should().BeFalse();
    }
}

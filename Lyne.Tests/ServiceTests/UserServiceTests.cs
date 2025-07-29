using AutoMapper;
using FluentAssertions;
using Lyne.Application.DTO;
using Lyne.Application.Services;
using Lyne.Domain.Entities;
using Lyne.Domain.IRepositories;
using Lyne.Infrastructure.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace Lyne.Tests.ServiceTests;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepoMock;
    private readonly IUserService _service;
    private readonly IMapper _mapper;
    private readonly Mock<ILogger<UserService>> _logger;

    public UserServiceTests()
    {
        _userRepoMock = new Mock<IUserRepository>();
        _logger = new Mock<ILogger<UserService>>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<Lyne.Application.Mapping.MappingProfile>();
        });
        _mapper = config.CreateMapper();


        _service = new UserService(_userRepoMock.Object, _mapper, _logger.Object);
    }
    [Fact]
    public async Task GetAllAsync_ReturnsListOfUsers_WhenUsersExists()
    {
        // Arrange
        _userRepoMock.Setup(r => r.GetAllAsync())
            .ReturnsAsync(new List<User>
            {
                new User { Id = 1, Name = "Test", ForName = "User", Email = "test@gmail.com", PasswordHash = "test", Genre = "test", Role = "User"},
                new User { Id = 2, Name = "Test2", ForName = "User2", Email = "test@gmail.com", PasswordHash = "test", Genre = "test", Role = "User"}
            });

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
            .ReturnsAsync(new User { Id = 1, Name = "Test", ForName = "User", Email = "test@gmail.com", PasswordHash = "test", Genre = "test", Role = "User"});

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

        _userRepoMock.Setup(r => r.AddAsync(It.IsAny<User>())).ReturnsAsync(true);
        _userRepoMock.Setup(r => r.ValidateForCreateAsync(It.IsAny<User>())).ReturnsAsync(true);

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
        _userRepoMock.Setup(r => r.ValidateForUpdateAsync(It.IsAny<User>())).ReturnsAsync(true);
        _userRepoMock.Setup(r => r.UpdateAsync(It.IsAny<User>())).ReturnsAsync(true);

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
        _userRepoMock.Setup(r => r.ExistsAsync(userDto.Id)).ReturnsAsync(true);
        _userRepoMock.Setup(r => r.ValidateForCreateAsync(It.IsAny<User>())).ReturnsAsync(false);

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
        _userRepoMock.Setup(r => r.GetByIdAsync(userDto.Id)).ReturnsAsync(
            new User { Id = userDto.Id,Name = userDto.Name,ForName = userDto.ForName,Email = userDto.Email,PasswordHash = userDto.PasswordHash, Genre = userDto.Genre, Role = "User"});
        _userRepoMock.Setup(r => r.DeleteAsync(It.IsAny<User>())).ReturnsAsync(true);

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

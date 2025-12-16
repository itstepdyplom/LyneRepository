using AutoMapper;
using FluentAssertions;
using Lyne.Application.DTO;
using Lyne.Application.DTO.Auth;
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
            cfg.AddProfile<Lyne.Application.Mapping.MappingProfile>());
        _mapper = config.CreateMapper();

        _service = new UserService(_userRepoMock.Object, _mapper, _logger.Object);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsUsers_WhenUsersExist()
    {
        _userRepoMock.Setup(r => r.GetAllAsync())
            .ReturnsAsync(new List<User>
            {
                new() { Id = 1, Name = "Test", ForName = "User", Email = "a@test.com", Gender = "male", PasswordHash = "", Role = "admin"},
                new() { Id = 2, Name = "Test2", ForName = "User2", Email = "b@test.com", Gender = "male", PasswordHash = "", Role = "admin"}
            });

        var result = await _service.GetAllAsync();

        result.Should().NotBeNull();
        result.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsEmptyList_WhenRepositoryReturnsNull()
    {
        _userRepoMock.Setup(r => r.GetAllAsync())
            .ReturnsAsync((List<User>)null!);

        var result = await _service.GetAllAsync();

        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsUserDto_WhenExists()
    {
        _userRepoMock.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(new User
            {
                Id = 1,
                Name = "Test",
                ForName = null,
                Gender = null,
                PasswordHash = null,
                Email = null,
                Role = null
            });

        var result = await _service.GetByIdAsync(1);

        result.Should().NotBeNull();
        result!.Name.Should().Be("Test");
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenNotExists()
    {
        _userRepoMock.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync((User?)null);

        var result = await _service.GetByIdAsync(1);

        result.Should().BeNull();
    }

    [Fact]
    public async Task AddAsync_ReturnsTrue_WhenRepositoryAdds()
    {
        var dto = new UserDto
        {
            Id = 1,
            Name = "Test",
            Email = "test@test.com"
        };

        _userRepoMock.Setup(r => r.AddAsync(It.IsAny<User>()))
            .ReturnsAsync(true);

        var result = await _service.AddAsync(dto);

        result.Should().BeTrue();
    }

    [Fact]
    public async Task AddAsync_ReturnsFalse_WhenRepositoryFails()
    {
        var dto = new UserDto { Name = "Test" };

        _userRepoMock.Setup(r => r.AddAsync(It.IsAny<User>()))
            .ReturnsAsync(false);

        var result = await _service.AddAsync(dto);

        result.Should().BeFalse();
    }

    [Fact]
    public async Task UpdateAsync_ReturnsTrue_WhenRepositoryUpdates()
    {
        var dto = new UserUpdateDto { Name = "Updated" };

        _userRepoMock.Setup(r => r.UpdateAsync(It.IsAny<User>()))
            .ReturnsAsync(true);

        var result = await _service.UpdateAsync(1,dto);

        result.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateAsync_ReturnsFalse_WhenDtoIsNull()
    {
        var result = await _service.UpdateAsync(0,null);

        result.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteAsync_ReturnsTrue_WhenRepositoryDeletes()
    {
        var dto = new User
        {
            Name = "Test",
            ForName = null,
            Gender = null,
            PasswordHash = null,
            Email = null,
            Role = null
        };

        _userRepoMock.Setup(r => r.DeleteAsync(dto))
            .ReturnsAsync(true);

        var result = await _service.DeleteAsync(1);

        result.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteAsync_ReturnsFalse_WhenRepositoryReturnsFalse()
    {
        var dto = new User
        {
            Name = "Test",
            ForName = null,
            Gender = null,
            PasswordHash = null,
            Email = null,
            Role = null
        };

        _userRepoMock.Setup(r => r.DeleteAsync(dto))
            .ReturnsAsync(false);

        var result = await _service.DeleteAsync(1);

        result.Should().BeFalse();
    }
}

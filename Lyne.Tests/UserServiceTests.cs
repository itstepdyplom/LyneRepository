using AutoMapper;
using Lyne.Domain.Entities;
using Moq;

namespace Lyne.Tests;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepoMock;
    private readonly IMapper _mapper;
    private readonly UserService _service;

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
    public async Task GetByIdAsync_ReturnsUserDto_WhenUserExists()
    {
        // Arrange
        var id = Guid.NewGuid();
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
        var id = Guid.NewGuid();
        _userRepoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((User?)null);

        var result = await _service.GetByIdAsync(id);

        result.Should().BeNull();
    }
}

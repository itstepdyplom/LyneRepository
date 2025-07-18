using Lyne.Application.DTO.Auth;
using Lyne.Application.Services;
using Lyne.Domain.Entities;
using Lyne.Domain.IRepositories;
using Microsoft.Extensions.Logging;
using Moq;

namespace Lyne.Tests.ServiceTests;

public class AuthServiceTests
{
    private readonly Mock<IAuthRepository> _authRepoMock;
    private readonly Mock<IJwtService> _jwtServiceMock;
    private readonly AuthService _authService;
    private readonly Mock<ILogger<AuthService>> _logger;

    public AuthServiceTests()
    {
        _authRepoMock = new Mock<IAuthRepository>();
        _jwtServiceMock = new Mock<IJwtService>();
        _logger = new Mock<ILogger<AuthService>>();
        _authService = new AuthService(_authRepoMock.Object, _jwtServiceMock.Object, _logger.Object);
    }

    [Fact]
    public async Task LoginAsync_ReturnsAuthResponse_WhenCredentialsAreValid()
    {
        var user = new User
        {
            Id = 1,
            Email = "test@example.com",
            PasswordHash = "hashedpassword123",
            Name = "Test",
            ForName = "User",
            Genre = "test"
        };
        _authRepoMock.Setup(r => r.GetUserByEmailAsync("test@example.com")).ReturnsAsync(user);
        _jwtServiceMock.Setup(j => j.GenerateToken(user)).Returns("token123");

        var loginRequest = new LoginRequestDto { Email = "test@example.com", Password = "password123" };

        var result = await _authService.LoginAsync(loginRequest);

        Assert.NotNull(result);
        Assert.Equal("token123", result!.Token);
        Assert.Equal("test@example.com", result.Email);
    }

    [Fact]
    public async Task LoginAsync_ReturnsNull_WhenUserNotFound()
    {
        _authRepoMock.Setup(r => r.GetUserByEmailAsync("notfound@example.com")).ReturnsAsync((User?)null);

        var loginRequest = new LoginRequestDto { Email = "notfound@example.com", Password = "password123" };

        var result = await _authService.LoginAsync(loginRequest);

        Assert.Null(result);
    }

    [Fact]
    public async Task LoginAsync_ReturnsNull_WhenPasswordIsInvalid()
    {
        var user = new User
        {
            Id = 1,
            Email = "test@example.com",
            PasswordHash = "hashedpassword123",
            Name = "Test",
            ForName = "User",
            Genre = "test"
        };
        _authRepoMock.Setup(r => r.GetUserByEmailAsync("test@example.com")).ReturnsAsync(user);

        var loginRequest = new LoginRequestDto { Email = "test@example.com", Password = "wrongpassword" };

        var result = await _authService.LoginAsync(loginRequest);

        Assert.Null(result);
    }

    [Fact]
    public async Task RegisterAsync_ReturnsAuthResponse_WhenRegistrationIsSuccessful()
    {
        _authRepoMock.Setup(r => r.UserExistsAsync("new@example.com")).ReturnsAsync(false);
        _authRepoMock.Setup(r => r.CreateAddressAsync(It.IsAny<Address>())).ReturnsAsync(new Address { Id = 1, Street = "test", City = "test", Zip = "test", State = "test", Country = "test" });
        _authRepoMock.Setup(r => r.CreateUserAsync(It.IsAny<User>()))
            .ReturnsAsync((User u) => new User
            {
                Id = 2,
                Name = u.Name,
                ForName = u.ForName,
                Email = u.Email,
                PasswordHash = u.PasswordHash,
                Genre = u.Genre,
                DateOfBirth = u.DateOfBirth,
                PhoneNumber = u.PhoneNumber,
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt,
                AddressId = u.AddressId
            });
        _jwtServiceMock.Setup(j => j.GenerateToken(It.IsAny<User>())).Returns("token456");

        var registerRequest = new RegisterRequestDto
        {
            Name = "New",
            ForName = "User",
            Email = "new@example.com",
            Password = "password123",
            ConfirmPassword = "password123",
            DateOfBirth = DateTime.UtcNow.AddYears(-20)
        };

        var result = await _authService.RegisterAsync(registerRequest);

        Assert.NotNull(result);
        Assert.Equal("token456", result!.Token);
        Assert.Equal("new@example.com", result.Email);
    }

    [Fact]
    public async Task RegisterAsync_ReturnsNull_WhenUserAlreadyExists()
    {
        _authRepoMock.Setup(r => r.UserExistsAsync("exists@example.com")).ReturnsAsync(true);

        var registerRequest = new RegisterRequestDto
        {
            Name = "Exists",
            ForName = "User",
            Email = "exists@example.com",
            Password = "password123",
            ConfirmPassword = "password123",
            DateOfBirth = DateTime.UtcNow.AddYears(-20)
        };

        var result = await _authService.RegisterAsync(registerRequest);

        Assert.Null(result);
    }

    [Fact]
    public async Task LoginWithGoogleAsync_ReturnsAuthResponse_WhenUserExists()
    {
        var existingUser = new User
        {
            Id = 1,
            Email = "googleuser@example.com",
            Name = "Existing User",
            ForName = "Google",
            Genre = "",
            PasswordHash = "",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _authRepoMock.Setup(r => r.GetUserByEmailAsync("googleuser@example.com")).ReturnsAsync(existingUser);
        _jwtServiceMock.Setup(j => j.GenerateToken(existingUser)).Returns("token_google_existing");

        var result = await _authService.LoginWithGoogleAsync("googleuser@example.com", "Existing User");

        Assert.NotNull(result);
        Assert.Equal("token_google_existing", result!.Token);
        Assert.Equal("googleuser@example.com", result.Email);
        Assert.Equal("Existing User", result.Name);
    }

    [Fact]
    public async Task LoginWithGoogleAsync_CreatesUserAndReturnsAuthResponse_WhenUserDoesNotExist()
    {
        User? createdUser = null;

        _authRepoMock.Setup(r => r.GetUserByEmailAsync("newgoogleuser@example.com")).ReturnsAsync((User?)null);
        _authRepoMock.Setup(r => r.CreateUserAsync(It.IsAny<User>()))
            .ReturnsAsync((User u) =>
            {
                u.Id = 42;
                createdUser = u;
                return u;
            });
        _jwtServiceMock.Setup(j => j.GenerateToken(It.IsAny<User>())).Returns("token_google_new");

        var result = await _authService.LoginWithGoogleAsync("newgoogleuser@example.com", "New Google User");

        Assert.NotNull(result);
        Assert.Equal("token_google_new", result!.Token);
        Assert.Equal("newgoogleuser@example.com", result.Email);
        Assert.Equal("New Google User", result.Name);

        Assert.NotNull(createdUser);
        Assert.Equal("newgoogleuser@example.com", createdUser!.Email);
        Assert.Equal("New Google User", createdUser.Name);
    }

}
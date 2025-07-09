using Lyne.Domain.Entities;
using Lyne.Domain.Enums;
using Lyne.Infrastructure.Persistence;
using Lyne.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Lyne.Tests.RepositoriesTests;

public class UserRepositoryTests : IAsyncLifetime
{
    private readonly AppDbContext _context;
    private readonly UserRepository _repository;
    private readonly Mock<ILogger<UserRepository>> _mockLogger;

    public UserRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;

        _context = new AppDbContext(options);
        _context.Database.OpenConnection();
        _context.Database.ExecuteSqlRaw("PRAGMA foreign_keys=ON;");
        _context.Database.EnsureCreated();

        _mockLogger = new Mock<ILogger<UserRepository>>();
        _repository = new UserRepository(_context, _mockLogger.Object);
    }

    public async Task InitializeAsync()
    {
        _context.Users.RemoveRange(_context.Users);
        await _context.SaveChangesAsync();
    }

    public Task DisposeAsync() => Task.CompletedTask;

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoUsers()
    {
        // Arrange

        // Act
        var users = await _repository.GetAllAsync();
        
        // Assert
        Assert.NotNull(users);
        Assert.Empty(users);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnUsers_WhenUsersExist()
    {
        // Arrange
        var user = new User
        {
            Name = "Test User",
            Email = "test@example.com",
            ForName = "Test",
            Genre = "M",
            PhoneNumber = "1234567890",
            PasswordHash = "test"
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Act
        var users = await _repository.GetAllAsync();
        
        // Assert
        Assert.NotEmpty(users);
        Assert.Contains(users, u => u.Name == "Test User");
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenUserNotFound()
    {
        // Arrange

        // Act
        var user = await _repository.GetByIdAsync(-1);
        
        // Assert
        Assert.Null(user);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var user = new User
        {
            Name = "Test User 2",
            Email = "test2@example.com",
            ForName = "Test2",
            Genre = "F",
            PhoneNumber = "0987654321",
            PasswordHash = "test2"
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetByIdAsync(user.Id);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(user.Name, result!.Name);
    }

    [Fact]
    public async Task AddAsync_ShouldReturnFalse_WhenUserIsNull()
    {
        // Arrange

        // Act
        var result = await _repository.AddAsync(null);
        
        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task AddAsync_ShouldAddUser_WhenUserIsValid()
    {
        // Arrange
        var user = new User
        {
            Name = "New User",
            Email = "newuser@example.com",
            ForName = "New",
            Genre = "M",
            PhoneNumber = "111222333",
            PasswordHash = "test"
        };

        // Act
        var result = await _repository.AddAsync(user);
        await _context.SaveChangesAsync();

        // Assert
        Assert.True(result);
        var savedUser = await _context.Users.FindAsync(user.Id);
        Assert.NotNull(savedUser);
    }

    [Fact]
    public async Task Update_ShouldReturnFalse_WhenUserIsNull()
    {
        // Arrange

        // Act
        var result = await _repository.Update(null);
        
        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task Update_ShouldReturnTrue_WhenUserExists()
    {
        // Arrange
        var user = new User
        {
            Name = "Update User",
            Email = "update@example.com",
            ForName = "Update",
            Genre = "M",
            PhoneNumber = "222333444",
            PasswordHash = "test"
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        user.Name = "Updated Name";
        
        // Act
        var result = await _repository.Update(user);
        
        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task Delete_ShouldReturnFalse_WhenUserIsNull()
    {
        // Arrange

        // Act
        var result = await _repository.Delete(null);
        
        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task Delete_ShouldReturnTrue_WhenUserExists()
    {
        // Arrange
        var user = new User
        {
            Name = "Delete User",
            Email = "delete@example.com",
            ForName = "Delete",
            Genre = "F",
            PhoneNumber = "555666777",
            PasswordHash = "test"
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.Delete(user);
        await _context.SaveChangesAsync();

        // Assert
        Assert.True(result);
        var deletedUser = await _context.Users.FindAsync(user.Id);
        Assert.Null(deletedUser);
    }

    [Fact]
    public async Task ExistsAsync_ShouldReturnFalse_WhenUserDoesNotExist()
    {
        // Arrange
        
        // Act
        var exists = await _repository.ExistsAsync(-999);
        
        // Assert
        Assert.False(exists);
    }

    [Fact]
    public async Task ExistsAsync_ShouldReturnTrue_WhenUserExists()
    {
        // Arrange
        var user = new User
        {
            Name = "Exists User",
            Email = "exists@example.com",
            ForName = "Exists",
            Genre = "M",
            PhoneNumber = "000111222",
            PasswordHash = "test"
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Act
        var exists = await _repository.ExistsAsync(user.Id);
        
        // Assert
        Assert.True(exists);
    }

    [Fact]
    public async Task ValidateForCreateAsync_ShouldReturnFalse_WhenRequiredFieldsMissing()
    {
        // Arrange
        var user = new User(){Name = "test", Email = "test", ForName = "test", PasswordHash = "test", Genre = "test"};
        
        // Act
        var isValid = await _repository.ValidateForCreateAsync(user);
        
        // Assert
        Assert.False(isValid);
    }

    [Fact]
    public async Task ValidateForCreateAsync_ShouldReturnTrue_WhenAllRequiredFieldsPresent()
    {
        // Arrange
        var user = new User
        {
            Name = "Valid User",
            Email = "valid@example.com",
            ForName = "Valid",
            Genre = "F",
            PhoneNumber = "999888777",
            PasswordHash = "test"
        };
        
        // Act
        var isValid = await _repository.ValidateForCreateAsync(user);
        
        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public async Task ValidateForUpdateAsync_ShouldReturnFalse_WhenUserNotExists()
    {
        // Arrange
        var user = new User
        {
            Id = -123,
            Name = "NoUser",
            Email = "no@example.com",
            ForName = "No",
            Genre = "M",
            PhoneNumber = "000000000",
            AddressId = 1,
            DateOfBirth = DateTime.UtcNow,
            PasswordHash = "test",
            Orders = new List<Order> { new Order() }
        };

        // Act
        var isValid = await _repository.ValidateForUpdateAsync(user);
        
        // Assert
        Assert.False(isValid);
    }

    [Fact]
    public async Task ValidateForUpdateAsync_ShouldReturnTrue_WhenUserExistsAndValid()
    {
        // Arrange
        var user = new User
        {
            Name = "Update Valid User",
            Email = "updatevalid@example.com",
            ForName = "UpdateValid",
            Genre = "F",
            PhoneNumber = "333222111",
            DateOfBirth = DateTime.UtcNow,
            PasswordHash = "test",
            Orders = new List<Order>()
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var address = new Address
        {
            City = "test",
            Country = "US",
            State = "test",
            Street = "test",
            Zip = "12345",
            UserId = user.Id
        };

        _context.Addresses.Add(address);
        await _context.SaveChangesAsync();

        user.AddressId = address.Id;
        await _context.SaveChangesAsync();

        var order = new Order
        {
            Date = DateTime.UtcNow,
            UserId = user.Id,
            ShippingAddressId = address.Id,
            PaymentMethod = "Credit Card",
            TrackingNumber = 123,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            OrderStatus = OrderStatus.Pending
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        user.Orders = new List<Order> { order };

        // Act
        var isValid = await _repository.ValidateForUpdateAsync(user);
        
        // Assert
        Assert.True(isValid);
    }
}

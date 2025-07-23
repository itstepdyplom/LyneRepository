using Lyne.Domain.Entities;
using Lyne.Infrastructure.Persistence;
using Lyne.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace Lyne.Tests.RepositoriesTests;

public class AddressRepositoryTests : IAsyncLifetime
{
    private readonly AppDbContext _context;
    private readonly Mock<ILogger<AddressRepository>> _mockLogger;
    private readonly AddressRepository _repository;

    public AddressRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;
        _context = new AppDbContext(options);
        _context.Database.OpenConnection();
        _context.Database.EnsureCreated();

        _mockLogger = new Mock<ILogger<AddressRepository>>();
        _repository = new AddressRepository(_context, _mockLogger.Object);
    }

    private async Task InitializeAsync()
    {
        _context.ChangeTracker.Clear();
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync(); // Recreate schema
        
        _context.Addresses.Add(new Address
            {  City = "test", State = "test", Country = "test", Street = "test", Zip = "test" });
        await _context.SaveChangesAsync();
    }

    Task IAsyncLifetime.InitializeAsync()
    {
        return InitializeAsync();
    }
    public Task DisposeAsync() => Task.CompletedTask;

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenAddressDoesNotExist()
    {
        // Arrange
        await InitializeAsync();

        // Act
        var result = await _repository.GetByIdAsync(999);
        
        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnAddress_WhenAddressExists()
    {
        // Arrange
        var address = new Address
        {
            City = "City",
            State = "State",
            Country = "Country",
            Street = "Street",
            Zip = "12345"
        };
        _context.Addresses.Add(address);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetByIdAsync(address.Id);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(address.Id, result.Id);
    }

    [Fact]
    public async Task Update_ShouldReturnFalse_WhenUserIsNull()
    {
        // Arrange
        var address = new Address { City = "test", Country = "test", Street = "test", State = "test", Zip = "12345" };

        // Act
        var result = await _repository.Update(address);
        
        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task Update_ShouldReturnTrue_WhenValid()
    {
        // Arrange
        var address = new Address
        {
            City = "City",
            State = "State",
            Country = "Country",
            Street = "Street",
            Zip = "Zip"
        };
        _context.Addresses.Add(address);
        await _context.SaveChangesAsync();

        // Act
        address.City = "New City";
        var result = await _repository.Update(address);

        // Assert
        Assert.True(result);

        var updated = await _context.Addresses.FindAsync(address.Id);
        Assert.Equal("New City", updated?.City);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnFalse_WhenValidationFails()
    {
        // Arrange
        var address = new Address { City = "test", Country = "test", Street = "test", State = "test", Zip = "12345" };
        
        // Act
        var result = await _repository.DeleteAsync(address);
        
        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnTrue_WhenAddressExists()
    {
        // Arrange
        var address = new Address
        {
            City = "City",
            State = "State",
            Country = "Country",
            Street = "Street",
            Zip = "Zip"
        };
        _context.Addresses.Add(address);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.DeleteAsync(address);

        // Assert
        Assert.True(result);

        var deleted = await _context.Addresses.FindAsync(address.Id);
        Assert.Null(deleted);
    }
    
    [Fact]
    public async Task ValidateForUpdateAsync_ShouldReturnFalse_WhenFieldsAreMissing()
    {
        // Arrange
        var address = new Address
        {
            City = "", // missing field
            Country = "test",
            Street = "test",
            State = "test",
            Zip = "12345"
        };
        _context.Addresses.Add(address);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.ValidateForUpdateAsync(address);
        
        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public async Task ValidateForUpdateAsync_ShouldReturnTrue_WhenAllValid()
    {
        // Arrange
        var address = new Address
        {
            City = "test",
            Country = "test",
            Street = "test",
            State = "test",
            Zip = "12345"
        };
        _context.Addresses.Add(address);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.ValidateForUpdateAsync(address);
        
        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task AddAsync_ShouldAddAddress_WhenValid()
    {
        // Arrange
        var address = new Address
        {
            City = "City",
            State = "State",
            Country = "Country",
            Street = "Street",
            Zip = "Zip"
        };
        
        // Act
        var result = await _repository.AddAsync(address);

        // Assert
        Assert.True(result);
        var saved = await _context.Addresses.FindAsync(address.Id);
        Assert.NotNull(saved);
    }

    [Fact]
    public async Task AddAsync_ShouldReturnFalse_WhenValidationError()
    {
        // Arrange
        var address = new Address
        {
            City = "", // invalid: required field missing
            State = "State",
            Country = "Country",
            Street = "Street",
            Zip = "Zip"
        };

        // Act
        var result = await _repository.AddAsync(address);

        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public async Task AddAsync_ShouldReturnFalse_WhenAddressIsNull()
    {
        // Arrange

        // Act
        var result = await _repository.AddAsync(null);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task ExistsAsync_ShouldReturnTrue_WhenAddressExists()
    {
        // Arrange
        var address = new Address
            { City = "City", State = "State", Country = "Country", Street = "Street", Zip = "Zip" };
        _context.Addresses.Add(address);
        await _context.SaveChangesAsync();

        // Act
        var exists = await _repository.ExistsAsync(address.Id);

        // Assert
        Assert.True(exists);
    }

    [Fact]
    public async Task ExistsAsync_ShouldReturnFalse_WhenAddressDoesNotExist()
    {
        // Act
        var exists = await _repository.ExistsAsync(9999);
        
        // Assert
        Assert.False(exists);
    }

    [Fact]
    public async Task ValidateForCreateAsync_ShouldReturnFalse_WhenUserIsNull()
    {
        // Arrange
        var address = new Address
        {
            City = "City",
            State = "State",
            Country = "Country",
            Street = "Street",
            Zip = "Zip"
        };
        
        // Act
        var result = await _repository.ValidateForCreateAsync(address);
        
        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task ValidateForCreateAsync_ShouldReturnFalse_WhenRequiredFieldsMissing()
    {
        // Arrange
        var address = new Address
        {
            City = "", // missing city
            State = "State",
            Country = "Country",
            Street = "Street",
            Zip = "Zip"
        };
        
        // Act
        var result = await _repository.ValidateForCreateAsync(address);
        
        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task ValidateForCreateAsync_ShouldReturnTrue_WhenValid()
    {
        // Arrange
        var address = new Address
        {
            City = "City",
            State = "State",
            Country = "Country",
            Street = "Street",
            Zip = "Zip"
        };
        
        // Act
        var result = await _repository.ValidateForCreateAsync(address);
        
        // Assert
        Assert.True(result);
    }
}

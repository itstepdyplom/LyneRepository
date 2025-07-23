using AutoMapper;
using Lyne.Domain.Entities;
using Lyne.Domain.Enums;
using Lyne.Infrastructure.Caching;
using Lyne.Infrastructure.Persistence;
using Lyne.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace Lyne.Tests.RepositoriesTests;

public class OrderRepositoryTests : IAsyncLifetime
{
    private readonly AppDbContext _context;
    private readonly Mock<ILogger<OrderRepository>> _mockLogger;
    private readonly OrderRepository _repository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ICacheService> _mockCache;

    public OrderRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;
        _context = new AppDbContext(options);
        _context.Database.OpenConnection();
        _context.Database.EnsureCreated();

        _mockLogger = new Mock<ILogger<OrderRepository>>();
        _mockMapper  = new Mock<IMapper>();
        _mockCache = new Mock<ICacheService>();
        _repository = new OrderRepository(_context, _mockLogger.Object, _mockMapper.Object,_mockCache.Object);
    }

    public async Task InitializeAsync()
    {
        _context.ChangeTracker.Clear();
        _context.Orders.RemoveRange(_context.Orders);
        await _context.SaveChangesAsync();
    }

    public Task DisposeAsync() => Task.CompletedTask;

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenOrderDoesNotExist()
    {
        // Arrange

        // Act
        var result = await _repository.GetByIdAsync(new int());
        
        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_ShouldAddOrder_WhenValid()
    {
        // Arrange
        var user = new User
        {
            Email = "test",
            ForName = "test",
            Name = "test",
            PasswordHash = "test",
            Genre = "test",
            Role = "User"
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var address = new Address
        {
            City = "test",
            Country = "test",
            Street = "test",
            State = "test",
            Zip = "12345",
        };

        _context.Addresses.Add(address);
        await _context.SaveChangesAsync();
        
        var order = new Order
        {
            Date = DateTime.UtcNow,
            UserId = user.Id,
            ShippingAddressId = address.Id,
            PaymentMethod = "Credit Card",
            TrackingNumber = 123456,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            OrderStatus = OrderStatus.Pending
        };

        // Act
        var result = await _repository.AddAsync(order);

        // Assert
        Assert.True(result);
        var saved = await _context.Orders.FindAsync(order.Id);
        Assert.NotNull(saved);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllOrders()
    {
        // Arrange
        var user = new User { Email = "test", ForName = "test", Name = "test", PasswordHash = "test", Genre = "test", Role = "User"};
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        
        var address = new Address { City = "test", Country = "test", Street = "test", State = "test", Zip = "12345" };
        _context.Addresses.Add(address);
        await _context.SaveChangesAsync();
        
        user.AddressId = address.Id;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        var order1 = new Order
        {
            Date = DateTime.UtcNow,
            UserId = user.Id,
            ShippingAddressId = address.Id,
            PaymentMethod = "Credit Card",
            TrackingNumber = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            OrderStatus = OrderStatus.Pending
        };
        var order2 = new Order
        {
            Date = DateTime.UtcNow,
            UserId = user.Id,
            ShippingAddressId = address.Id,
            PaymentMethod = "Credit Card",
            TrackingNumber = 2,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            OrderStatus = OrderStatus.Pending
        };
        _context.Orders.AddRange(order1, order2);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task Update_ShouldReturnFalse_WhenOrderDoesNotExist()
    {
        // Arrange
        var order = new Order { Id = new int() };
        
        // Act
        var result = await _repository.Update(order);
        
        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public async Task Update_ShouldReturnTrue_WhenOrderExists()
    {
        // Arrange
        var user = new User { Email = "test", ForName = "test", Name = "test", PasswordHash = "test", Genre = "test", Role = "User" };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var address = new Address { City = "test", Country = "test", Street = "test", State = "test", Zip = "12345" };
        _context.Addresses.Add(address);
        await _context.SaveChangesAsync();

        var order = new Order
        {
            Date = DateTime.UtcNow,
            UserId = user.Id,
            ShippingAddressId = address.Id,
            PaymentMethod = "Credit Card",
            TrackingNumber = 123456,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            OrderStatus = OrderStatus.Pending
        };
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        order.PaymentMethod = "Paypal";

        // Act
        var result = await _repository.Update(order);

        // Assert
        Assert.True(result);

        var updatedOrder = await _context.Orders.FindAsync(order.Id);
        Assert.Equal("Paypal", updatedOrder.PaymentMethod);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnFalse_WhenOrderDoesNotExist()
    {
        // Arrange
        var order = new Order { Id = new int() };
        
        // Act
        var result = await _repository.DeleteAsync(order);
        
        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public async Task DeleteAsync_ShouldReturnTrue_WhenOrderExists()
    {
        // Arrange: створюємо користувача, адресу та замовлення
        var user = new User { Email = "test", ForName = "test", Name = "test", PasswordHash = "test", Genre = "test", Role = "User"};
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var address = new Address { City = "test", Country = "test", Street = "test", State = "test", Zip = "12345" };
        _context.Addresses.Add(address);
        await _context.SaveChangesAsync();

        var order = new Order
        {
            Date = DateTime.UtcNow,
            UserId = user.Id,
            ShippingAddressId = address.Id,
            PaymentMethod = "Credit Card",
            TrackingNumber = 123456,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            OrderStatus = OrderStatus.Pending
        };
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.DeleteAsync(order);

        // Assert
        Assert.True(result);

        var deletedOrder = await _context.Orders.FindAsync(order.Id);
        Assert.Null(deletedOrder);
    }
    
    [Fact]
    public async Task ValidateForCreateAsync_ShouldReturnTrue_WhenOrderIsValid()
    {
        // Arrange
        var order = new Order
        {
            OrderStatus = OrderStatus.Pending, // Не default
            PaymentMethod = "Credit Card",
            ShippingAddressId = 1,
            TrackingNumber = 123456,
            UserId = 1
        };

        // Act
        var result = await _repository.ValidateForCreateAsync(order);

        // Assert
        Assert.True(result);
    }
    
    [Theory]
    [InlineData(OrderStatus.Pending, "Credit Card", 1, 123456, 1, true)] 
    [InlineData(OrderStatus.Pending, "", 1, 123456, 1, false)]           
    [InlineData(OrderStatus.Pending, null, 1, 123456, 1, false)]          
    [InlineData(default(OrderStatus), "Credit Card", 1, 123456, 1, false)]
    [InlineData(OrderStatus.Pending, "Credit Card", 0, 123456, 1, false)]
    [InlineData(OrderStatus.Pending, "Credit Card", 1, 0, 1, false)]     
    [InlineData(OrderStatus.Pending, "Credit Card", 1, 123456, 0, false)] 
    public async Task ValidateForCreateAsync_VariousInputs_ReturnsExpected(
        OrderStatus status,
        string paymentMethod,
        int shippingAddressId,
        int trackingNumber,
        int userId,
        bool expected)
    {
        // Arrange
        var order = new Order
        {
            OrderStatus = status,
            PaymentMethod = paymentMethod,
            ShippingAddressId = shippingAddressId,
            TrackingNumber = trackingNumber,
            UserId = userId
        };

        // Act
        var result = await _repository.ValidateForCreateAsync(order);

        // Assert
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public async Task ValidateForUpdateAsync_ShouldReturnTrue_ForValidOrder()
    {
        // Arrange
        var user = new User { Email = "test", ForName = "test", Name = "test", PasswordHash = "test", Genre = "test", Role = "User" };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var address = new Address { City = "test", Country = "test", Street = "test", State = "test", Zip = "12345" };
        _context.Addresses.Add(address);
        await _context.SaveChangesAsync();

        var order = new Order
        {
            Date = DateTime.UtcNow,
            UserId = user.Id,
            ShippingAddressId = address.Id,
            PaymentMethod = "Paypal",
            TrackingNumber = 123456,
            OrderStatus = OrderStatus.Pending
        };
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        Assert.True(order.Id > 0, "Order Id should be set after SaveChangesAsync");

        // Act
        var isValid = await _repository.ValidateForUpdateAsync(order);

        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public async Task ExistsAsync_ShouldReturnFalse_WhenOrderDoesNotExist()
    {
        // Arrange
        
        // Act
        var exists = await _repository.ExistsAsync(new int());
        
        // Assert
        Assert.False(exists);
    }
}

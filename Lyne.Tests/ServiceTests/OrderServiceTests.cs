using AutoMapper;
using FluentAssertions;
using Lyne.Application.DTO;
using Lyne.Application.Services;
using Lyne.Domain.Entities;
using Lyne.Domain.Enums;
using Lyne.Domain.IRepositories;
using Microsoft.Extensions.Logging;
using Moq;

namespace Lyne.Tests.ServiceTests;

public class OrderServiceTests
{
    private readonly Mock<IOrderRepository> _orderRepoMock;
    private readonly IOrderService _service;
    private readonly IMapper _mapper;
    private readonly Mock<ILogger<OrderService>> _logger;

    public OrderServiceTests()
    {
        _orderRepoMock = new Mock<IOrderRepository>();
        var config = new MapperConfiguration(cfg => { cfg.AddProfile<Lyne.Application.Mapping.MappingProfile>(); });
        _mapper = config.CreateMapper();
        _logger = new Mock<ILogger<OrderService>>();
        _service = new OrderService(_orderRepoMock.Object, _mapper, _logger.Object);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsOrders_WhenOrdersExists()
    {
        // Arrange
        var orders=new List<Order>(){ 
            new Order 
            {
            Id = 1,
            CreatedAt = new DateTime(), 
            UpdatedAt = new DateTime(),
            ShippingAddressId = 1
        },
            new Order
            {
                Id = 2,
                CreatedAt = new DateTime(),
                UpdatedAt = new DateTime(),
                ShippingAddressId = 2
            }, 
            new Order
            {
                Id = 3,
                CreatedAt = new DateTime(),
                UpdatedAt = new DateTime(),
                ShippingAddressId = 3
            }};
        _orderRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(orders);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        result.Should().NotBeNull();
    }
    
    [Fact]
    public async Task GetAllAsync_ReturnEmpty_WheOrdersNotExists()
    {
        // Arrange
        _orderRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Order>());
        
        // Act
        var result = await _service.GetAllAsync();

        // Assert
        result.Should().BeEmpty();
    }
    
    [Fact]
    public async Task GetAllAsync_ReturnsEmptyList_WhenRepositoryReturnsEmptyList()
    {
        // Arrange
        _orderRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Order>());

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        result.Should().BeEmpty();
    }
    
    [Fact]
    public async Task GetByIdAsync_ReturnsOrderDto_WhenOrderExists()
    {
        // Arrange
        var id = 1;
        _orderRepoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(new Order { Id = id });

        // Act
        var result = await _service.GetByIdAsync(id);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(id);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenOrderNotExists()
    {
        // Arrange
        var id = 1;
        _orderRepoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Order?)null);

        // Act
        var result = await _service.GetByIdAsync(id);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task AddAsync_ReturnsTrue_WhenOrderCreated()
    {
        // Arrange
        var orderDto = new OrderDto { Id = 1,OrderStatus = OrderStatus.Pending,Date = new DateTime(2000,01,01)};
        _orderRepoMock.Setup(r => r.ValidateForCreateAsync(It.IsAny<Order>())).ReturnsAsync(true);
        _orderRepoMock.Setup(r => r.AddAsync(It.IsAny<Order>())).ReturnsAsync(true);

        // Act
        var result = await _service.AddAsync(orderDto);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task AddAsync_ReturnsFalse_WhenValidationFails()
    {
        // Arrange
        var orderDto = new OrderDto { Id = 1,OrderStatus = OrderStatus.Pending,Date = new DateTime(2000,01,01)};
        _orderRepoMock.Setup(r => r.ValidateForCreateAsync(It.IsAny<Order>())).ReturnsAsync(false);

        // Act
        var result = await _service.AddAsync(orderDto);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public async Task AddAsync_ReturnsFalse_WhenOrderDtoIsNull()
    {
        // Arrange
        var order = new OrderDto(){ Date = new DateTime(2000,01,01), OrderStatus = OrderStatus.Pending};

        // Act
        var result = await _service.AddAsync(order);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task AddAsync_ThrowsException_WhenRepositoryThrows()
    {      
        // Arrange
        var order = new OrderDto(){ Date = new DateTime(2000,01,01), OrderStatus = OrderStatus.Pending};

        _orderRepoMock.Setup(r => r.AddAsync(It.IsAny<Order>())).ThrowsAsync(new Exception());

        // Act
        var result = await _service.AddAsync(order);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public async Task UpdateAsync_ReturnsTrue_WhenOrderIsUpdated()
    {
        // Arrange
        var orderDto = new OrderDto { Id = 1,OrderStatus = OrderStatus.Pending,Date = new DateTime(2000,01,01)};
        _orderRepoMock.Setup(r => r.ExistsAsync(orderDto.Id)).ReturnsAsync(true);
        _orderRepoMock.Setup(r => r.ValidateForUpdateAsync(It.IsAny<Order>())).ReturnsAsync(true);
        _orderRepoMock.Setup(r => r.Update(It.IsAny<Order>())).ReturnsAsync(true);

        // Act
        var result = await _service.UpdateAsync(orderDto);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateAsync_ReturnsFalse_WhenOrderNotExists()
    {
        // Arrange
        var orderDto = new OrderDto { Id = 1,OrderStatus = OrderStatus.Pending,Date = new DateTime(2000,01,01)};
        _orderRepoMock.Setup(r => r.ExistsAsync(orderDto.Id)).ReturnsAsync(false);

        // Act
        var result = await _service.UpdateAsync(orderDto);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public async Task UpdateAsync_ReturnsFalse_WhenThereIsValidationError()
    {
        // Arrange
        var orderDto = new OrderDto { Id = 1,OrderStatus = OrderStatus.Pending,Date = new DateTime(2000,01,01)};
        _orderRepoMock.Setup(r => r.ExistsAsync(orderDto.Id)).ReturnsAsync(true);
        _orderRepoMock.Setup(r => r.ValidateForCreateAsync(It.IsAny<Order>())).ReturnsAsync(false);

        // Act
        var result = await _service.UpdateAsync(orderDto);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public async Task UpdateAsync_ReturnsFalse_WhenOrderDtoIsNull()
    {
        // Act
        var result = await _service.UpdateAsync(null);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteAsync_ReturnsTrue_WhenOrderDeleted()
    {
        // Arrange
        var id = 1;
        var order = new Order { Id = id, Date = new DateTime(2000, 01, 01), OrderStatus = OrderStatus.Pending };
        _orderRepoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(order);
        _orderRepoMock.Setup(r => r.DeleteAsync(It.IsAny<Order>())).ReturnsAsync(true);

        // Act
        var result = await _service.DeleteAsync(id);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteAsync_ReturnsFalse_WhenOrderNotExists()
    {
        // Arrange
        var id = 1;
        _orderRepoMock.Setup(r => r.ExistsAsync(id)).ReturnsAsync(false);

        // Act
        var result = await _service.DeleteAsync(id);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public async Task DeleteAsync_ReturnsFalse_WhenIdIsEmptyGuid()
    {
        // Act
        var result = await _service.DeleteAsync(new int());

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteAsync_ThrowsException_WhenRepositoryThrows()
    {
        // Arrange
        var id = 1;
        var order = new Order() { Id = id, OrderStatus = OrderStatus.Pending,  Date = new DateTime(2000,01,01)};
        _orderRepoMock.Setup(r => r.ExistsAsync(id)).ReturnsAsync(true);
        _orderRepoMock.Setup(r => r.DeleteAsync(order)).ThrowsAsync(new Exception());


        // Act
        var result = await _service.DeleteAsync(id);

        // Assert
        result.Should().BeFalse();
    }
}
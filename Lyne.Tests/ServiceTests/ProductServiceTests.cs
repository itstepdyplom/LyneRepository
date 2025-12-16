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

public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _productRepoMock;
    private readonly IProductService _service;
    private readonly IMapper _mapper;
    private readonly Mock<ILogger<ProductService>> _logger;

    public ProductServiceTests()
    {
        _productRepoMock = new Mock<IProductRepository>();
        _logger = new Mock<ILogger<ProductService>>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<Lyne.Application.Mapping.MappingProfile>();
        });
        _mapper = config.CreateMapper();

        _service = new ProductService(_productRepoMock.Object, _mapper, _logger.Object);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsListOfProducts_WhenProductsExist()
    {
        // Arrange
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Category",
            Description = "Category description",
            Products = new List<Product>()
        };

        _productRepoMock
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(new List<Product?>
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Brand = "test",
                    Color = "test",
                    Category = category,
                    CategoryId = category.Id,
                    Description = "test",
                    ImageUrl = "test",
                    IsActive = true,
                    Name = "test",
                    Price = 1,
                    StockQuantity = 1,
                    Size = "test"
                }
            });

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        result.Should().NotBeNull();
        result.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsEmptyList_WhenRepositoryReturnsNull()
    {
        // Arrange
        _productRepoMock
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync((List<Product?>)null!);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsEmptyList_WhenRepositoryReturnsEmptyList()
    {
        // Arrange
        _productRepoMock
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(new List<Product?>());

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsProductDto_WhenProductExists()
    {
        // Arrange
        var id = Guid.NewGuid();
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Category",
            Description = "Category description",
            Products = new List<Product>()
        };

        _productRepoMock
            .Setup(r => r.GetByIdAsync(id))
            .ReturnsAsync(new Product
            {
                Id = id,
                Brand = "test",
                Color = "test",
                Category = category,
                CategoryId = category.Id,
                Description = "test",
                ImageUrl = "test",
                IsActive = true,
                Name = "Test",
                Price = 1,
                StockQuantity = 1,
                Size = "test"
            });

        // Act
        var result = await _service.GetByIdAsync(id);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(id);
        result.Name.Should().Be("Test");
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenProductDoesNotExist()
    {
        // Arrange
        var id = Guid.NewGuid();

        _productRepoMock
            .Setup(r => r.GetByIdAsync(id))
            .ReturnsAsync((Product?)null);

        // Act
        var result = await _service.GetByIdAsync(id);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task AddAsync_ReturnsOkTrue_AndCreatedProduct_WhenRepositoryAdds()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var dto = new ProductDto
        {
            Id = Guid.NewGuid(),
            Name = "test",
            Brand = "test",
            Color = "test",
            CategoryId = categoryId,
            Description = "test",
            ImageUrl = "test",
            IsActive = true,
            Price = 1,
            StockQuantity = 1,
            Size = "test"
        };

        _productRepoMock
            .Setup(r => r.AddAsync(It.IsAny<Product>()))
            .ReturnsAsync(true);

        // Act
        var (ok, created) = await _service.AddAsync(dto);

        // Assert
        ok.Should().BeTrue();
        created.Should().NotBeNull();
        created.Name.Should().Be("test");
        created.CreatedAt.Should().NotBe(default);
        created.UpdatedAt.Should().NotBe(default);

        _productRepoMock.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Once);
    }

    [Fact]
    public async Task AddAsync_ReturnsOkFalse_WhenRepositoryRejects()
    {
        // Arrange
        var dto = new ProductDto
        {
            Id = Guid.NewGuid(),
            Name = "test",
            Brand = "test",
            CategoryId = Guid.NewGuid(),
            Price = 1,
            StockQuantity = 1,
            Size = "M"
        };

        _productRepoMock
            .Setup(r => r.AddAsync(It.IsAny<Product>()))
            .ReturnsAsync(false);

        // Act
        var (ok, created) = await _service.AddAsync(dto);

        // Assert
        ok.Should().BeFalse();
        created.Should().NotBeNull();
        _productRepoMock.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Once);
    }

    [Fact]
    public async Task AddAsync_Throws_WhenDtoIsNull()
    {
        // Arrange
        ProductDto dto = null!;

        // Act
        var act = async () => await _service.AddAsync(dto);

        // Assert
        await act.Should().ThrowAsync<Exception>(); // mapper або NRE (бо p буде null)
    }

    [Fact]
    public async Task UpdateAsync_ReturnsTrue_WhenRepositoryUpdates()
    {
        // Arrange
        var dto = new ProductDto
        {
            Id = Guid.NewGuid(),
            Name = "test",
            Brand = "test",
            CategoryId = Guid.NewGuid(),
            Price = 1,
            StockQuantity = 1,
            Size = "M"
        };

        _productRepoMock
            .Setup(r => r.Update(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _service.UpdateAsync(dto);

        // Assert
        result.Should().BeTrue();
        _productRepoMock.Verify(r => r.Update(It.IsAny<Product>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsFalse_WhenDtoIsNull()
    {
        // Act
        var result = await _service.UpdateAsync(null);

        // Assert
        result.Should().BeFalse();
        _productRepoMock.Verify(r => r.Update(It.IsAny<Product>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsFalse_WhenRepositoryReturnsFalse()
    {
        // Arrange
        var dto = new ProductDto
        {
            Id = Guid.NewGuid(),
            Name = "test",
            Brand = "test",
            CategoryId = Guid.NewGuid(),
            Price = 1,
            StockQuantity = 1,
            Size = "M"
        };

        _productRepoMock
            .Setup(r => r.Update(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _service.UpdateAsync(dto);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteAsync_ReturnsTrue_WhenRepositoryDeletes()
    {
        // Arrange
        var id = Guid.NewGuid();

        // DeleteAsync всередині викликає GetByIdAsync, але результат не впливає на видалення.
        _productRepoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Product?)null);

        _productRepoMock
            .Setup(r => r.DeleteAsync(id))
            .ReturnsAsync(true);

        // Act
        var result = await _service.DeleteAsync(id);

        // Assert
        result.Should().BeTrue();
        _productRepoMock.Verify(r => r.DeleteAsync(id), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsFalse_WhenRepositoryReturnsFalse()
    {
        // Arrange
        var id = Guid.NewGuid();
        _productRepoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Product?)null);

        _productRepoMock
            .Setup(r => r.DeleteAsync(id))
            .ReturnsAsync(false);

        // Act
        var result = await _service.DeleteAsync(id);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteAsync_Throws_WhenRepositoryThrows()
    {
        // Arrange
        var id = Guid.NewGuid();
        _productRepoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Product?)null);

        _productRepoMock
            .Setup(r => r.DeleteAsync(id))
            .ThrowsAsync(new Exception("boom"));

        // Act
        var act = async () => await _service.DeleteAsync(id);

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }
}

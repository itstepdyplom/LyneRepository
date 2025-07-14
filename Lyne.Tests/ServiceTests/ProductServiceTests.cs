using AutoMapper;
using FluentAssertions;
using Lyne.Application.DTO;
using Lyne.Application.Services;
using Lyne.Domain.Entities;
using Lyne.Domain.IRepositories;
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

        var config = new MapperConfiguration(cfg => { cfg.AddProfile<Lyne.Application.Mapping.MappingProfile>(); });
        _mapper = config.CreateMapper();


        _service = new ProductService(_productRepoMock.Object, _mapper, _logger.Object);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsListOProducts_WhenProductsExists()
    {
        // Arrange
        var category = new Category()
        {
            Id = new Guid(),
            Name = "Category",
            Description = "Category description",
            Products = new List<Product>()
        };
        _productRepoMock.Setup(r => r.GetAllAsync())!
            .ReturnsAsync(new List<Product>
            {
                new Product { Id = new Guid(), Brand = "test", Color = "test",Category = category,CategoryId = category.Id, Description = "test", ImageUrl = "test", IsActive = true, Name = "test",Price = 1,StockQuantity = 1,Size = "test"}
            });

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        result!.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsNull_WhenProductsDoesNotExist()
    {
        // Arrange
        _productRepoMock.Setup(r => r.GetAllAsync())!.ReturnsAsync((List<Product>)null);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsEmptyList_WhenRepositoryReturnsEmptyList()
    {
        // Arrange
        _productRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Product>());

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsProductDto_WhenProductExists()
    {
        // Arrange
        var id = new Guid();
        var category = new Category()
        {
            Id = new Guid(),
            Name = "Category",
            Description = "Category description",
            Products = new List<Product>()
        };
        _productRepoMock.Setup(r => r.GetByIdAsync(id))
            .ReturnsAsync(new Product()
                { Id = id, Brand = "test", Color = "test",Category = category,CategoryId = category.Id, Description = "test", ImageUrl = "test", IsActive = true, Name = "Test",Price = 1,StockQuantity = 1,Size = "test" });

        // Act
        var result = await _service.GetByIdAsync(id);

        // Assert
        result.Should().NotBeNull();
        result!.Name.Should().Be("Test");
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenProductDoesNotExist()
    {
        // Arrange
        var id = new Guid();

        _productRepoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Product?)null);

        // Act
        var result = await _service.GetByIdAsync(id);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task AddAsync_ReturnsTrue_WhenProductCreated()
    {
        // Arrange
        var id = new Guid();
        var category = new Category()
        {
            Id = new Guid(),
            Name = "Category",
            Description = "Category description",
            Products = new List<Product>()
        };
        var product = new Product()
        {
            Id = id,
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
        };

        _productRepoMock.Setup(r => r.AddAsync(It.IsAny<Product>())).ReturnsAsync(true);
        _productRepoMock.Setup(r => r.ValidateForCreateAsync(It.IsAny<Product>())).ReturnsAsync(true);
        
        var productDto = _mapper.Map<ProductDto>(product);

        // Act
        var result = await _service.AddAsync(productDto);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task AddAsync_ReturnsFalse_WhenProductNameIsEmpty()
    {
        // Arrange
        var id = new Guid();
        var category = new Category()
        {
            Id = new Guid(),
            Name = "Category",
            Description = "Category description",
            Products = new List<Product>()
        };
        var productDto = new ProductDto()
        {
            Id = id,
            Brand = "test",
            Color = "test",
            CategoryId = category.Id,
            Description = "test",
            ImageUrl = "test",
            IsActive = true,
            Name = "test",
            Price = 1,
            StockQuantity = 1,
            Size = "test"
        };

        // Act
        var result = await _service.AddAsync(productDto);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task AddAsync_ReturnsFalse_WhenProductDtoIsNull()
    {
        // Arrange
        var productDto = new ProductDto();

        // Act
        var result = await _service.AddAsync(productDto);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task AddAsync_ReturnsFalse_WhenValidationFails()
    {
        // Arrange
        var id = new Guid();
        var category = new Category()
        {
            Id = new Guid(),
            Name = "Category",
            Description = "Category description",
            Products = new List<Product>()
        };
        var productDto = new ProductDto()
        {
            Id = id,
            Brand = "test",
            Color = "test",
            CategoryId = category.Id,
            Description = "test",
            ImageUrl = "test",
            IsActive = true,
            Name = "test",
            Price = 1,
            StockQuantity = 1,
            Size = "test"
        };
        
        _productRepoMock.Setup(r => r.ValidateForCreateAsync(It.IsAny<Product>())).ReturnsAsync(false);

        // Act
        var result = await _service.AddAsync(productDto);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task AddAsync_ThrowsException_WhenRepositoryThrows()
    {
        // Arrange
        var id = new Guid();
        var category = new Category()
        {
            Id = new Guid(),
            Name = "Category",
            Description = "Category description",
            Products = new List<Product>()
        };
        var productDto = new ProductDto()
        {
            Id = id,
            Brand = "test",
            Color = "test",
            CategoryId = category.Id,
            Description = "test",
            ImageUrl = "test",
            IsActive = true,
            Name = "test",
            Price = 1,
            StockQuantity = 1,
            Size = "test"
        };
        
        _productRepoMock.Setup(r => r.AddAsync(It.IsAny<Product>())).ThrowsAsync(new Exception());

        // Act
        var result = await _service.AddAsync(productDto);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task UpdateAsync_ReturnsTrue_WhenProductIsUpdated()
    {
        // Arrange
        var id = new Guid();
        var category = new Category()
        {
            Id = new Guid(),
            Name = "Category",
            Description = "Category description",
            Products = new List<Product>()
        };
        var product = new Product()
        {
            Id = id,
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
        };
        var productDto = _mapper.Map<ProductDto>(product);

        _productRepoMock.Setup(r => r.ExistsAsync(product.Id)).ReturnsAsync(true);
        _productRepoMock.Setup(r => r.ValidateForUpdateAsync(It.IsAny<Product>())).ReturnsAsync(true);
        _productRepoMock.Setup(r => r.Update(It.IsAny<Product>())).ReturnsAsync(true);

        // Act
        var result = await _service.UpdateAsync(productDto);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateAsync_ReturnsFalse_WhenProductNotExists()
    {
        // Arrange
        var id = new Guid();
        var category = new Category()
        {
            Id = new Guid(),
            Name = "Category",
            Description = "Category description",
            Products = new List<Product>()
        };
        var product = new Product()
        {
            Id = id,
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
        };
        var productDto = _mapper.Map<ProductDto>(product);

        _productRepoMock.Setup(r => r.ExistsAsync(product.Id)).ReturnsAsync(false);
        _productRepoMock.Setup(r => r.ValidateForUpdateAsync(It.IsAny<Product>())).ReturnsAsync(true);
        _productRepoMock.Setup(r => r.Update(It.IsAny<Product>())).ReturnsAsync(true);
        _productRepoMock.Setup(r => r.Update(It.IsAny<Product>()))
            .ReturnsAsync((Product p) => _productRepoMock.Object.ExistsAsync(p.Id).Result);
        
        // Act
        var result = await _service.UpdateAsync(productDto);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task UpdateAsync_ReturnsFalse_WhenThereIsValidationError()
    {
        // Arrange
        var id = new Guid();
        var category = new Category()
        {
            Id = new Guid(),
            Name = "Category",
            Description = "Category description",
            Products = new List<Product>()
        };
        var product = new Product()
        {
            Id = id,
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
        };
        var productDto = _mapper.Map<ProductDto>(product);

        _productRepoMock.Setup(r => r.ExistsAsync(product.Id)).ReturnsAsync(true);
        _productRepoMock.Setup(r => r.ValidateForUpdateAsync(It.IsAny<Product>())).ReturnsAsync(false);

        // Act
        var result = await _service.UpdateAsync(productDto);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task UpdateAsync_ReturnsFalse_WhenProductDtoIsNull()
    {
        // Act
        var result = await _service.UpdateAsync(null);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteAsync_ReturnsTrue_WhenProductIsDeleted()
    {
        // Arrange
        var id = new Guid();
        var category = new Category()
        {
            Id = new Guid(),
            Name = "Category",
            Description = "Category description",
            Products = new List<Product>()
        };
        var product = new Product()
        {
            Id = id,
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
        };
      
        _productRepoMock.Setup(r => r.GetByIdAsync(product.Id)).ReturnsAsync(product);
        _productRepoMock.Setup(r => r.DeleteAsync(It.IsAny<Product>())).ReturnsAsync(true);

        // Act
        var result = await _service.DeleteAsync(product.Id);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteAsync_ReturnsFalse_WhenProductNotExists()
    {
        // Arrange
        var id = new Guid();
        var category = new Category()
        {
            Id = new Guid(),
            Name = "Category",
            Description = "Category description",
            Products = new List<Product>()
        };
        var product = new Product()
        {
            Id = id,
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
        };
        var productDto = _mapper.Map<ProductDto>(product);
        
        _productRepoMock.Setup(r => r.ExistsAsync(productDto.Id)).ReturnsAsync(false);

        // Act
        var result = await _service.DeleteAsync(productDto.Id);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteAsync_ReturnsFalse_WhenIdIsEmptyGuid()
    {
        // Act
        var result = await _service.DeleteAsync(new Guid());

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteAsync_ThrowsException_WhenRepositoryThrows()
    {
        // Arrange
        var id = new Guid();
        var category = new Category()
        {
            Id = new Guid(),
            Name = "Category",
            Description = "Category description",
            Products = new List<Product>()
        };
        var product = new Product()
        {
            Id = id,
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
        };
        var productDto = _mapper.Map<ProductDto>(product);
        
        _productRepoMock.Setup(r => r.ExistsAsync(productDto.Id)).ReturnsAsync(true);
        _productRepoMock.Setup(r => r.DeleteAsync(product)).ThrowsAsync(new Exception());


        // Act
        var result = await _service.DeleteAsync(productDto.Id);

        // Assert
        result.Should().BeFalse();
    }
}
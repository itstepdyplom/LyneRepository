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

public class CategoryServiceTests
{
    private readonly Mock<ICategoryRepository> _categoryRepoMock;
    private readonly ICategoryService _service;
    private readonly IMapper _mapper;
    private readonly Mock<ILogger<CategoryService>> _logger;

    public CategoryServiceTests()
    {
        _categoryRepoMock = new Mock<ICategoryRepository>();
        var config = new MapperConfiguration(cfg => { cfg.AddProfile<Lyne.Application.Mapping.MappingProfile>(); });
        _mapper = config.CreateMapper();
        _logger = new Mock<ILogger<CategoryService>>();
        _service = new CategoryService(_categoryRepoMock.Object, _mapper, _logger.Object);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsOrders_WhenOrdersExists()
    {
        // Arrange
        var id = new Guid();
        var categories=new List<Category>(){ new Category{ Id = new Guid(),Name = "test"}, new Category{ Id = new Guid(),Name = "test" }, new Category{ Id = new Guid(),Name = "test" }};
        _categoryRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(categories);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        result.Should().NotBeNull();
    }
    
    [Fact]
    public async Task GetAllAsync_ReturnEmpty_WheOrdersNotExists()
    {
        // Arrange
        _categoryRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Category>());
        
        // Act
        var result = await _service.GetAllAsync();

        // Assert
        result.Should().BeEmpty();
    }
    
    [Fact]
    public async Task GetAllAsync_ReturnsEmptyList_WhenRepositoryReturnsEmptyList()
    {
        // Arrange
        _categoryRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Category>());

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        result.Should().BeEmpty();
    }
    
    [Fact]
    public void Products_Getter_ReturnsAssignedProducts()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = Guid.NewGuid(), Name = "Product1" },
            new Product { Id = Guid.NewGuid(), Name = "Product2" }
        };
        var category = new Category { Id = new Guid(), Name = "test", Products = products };

        // Act
        var result = category.Products;

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.Should().BeEquivalentTo(products);
    }
    
    [Fact]
    public async Task GetByIdAsync_ReturnsOrderDto_WhenOrderExists()
    {
        // Arrange
        var id = new Guid();
        _categoryRepoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(new Category() { Id = id,Name = "test" });

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
        var id = new Guid();
        _categoryRepoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Category?)null);

        // Act
        var result = await _service.GetByIdAsync(id);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task AddAsync_ReturnsTrue_WhenOrderCreated()
    {
        // Arrange
        var categoryDto = new CategoryDto { Id = new Guid(),Name = "test"};
        _categoryRepoMock.Setup(r => r.ValidateForCreateAsync(It.IsAny<Category>())).ReturnsAsync(true);
        _categoryRepoMock.Setup(r => r.AddAsync(It.IsAny<Category>())).ReturnsAsync(true);

        // Act
        var result = await _service.AddAsync(categoryDto);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task AddAsync_ReturnsFalse_WhenValidationFails()
    {
        // Arrange
        var categoryDto = new CategoryDto { Id = new Guid(),Name = "test"};
        _categoryRepoMock.Setup(r => r.ValidateForCreateAsync(It.IsAny<Category>())).ReturnsAsync(false);

        // Act
        var result = await _service.AddAsync(categoryDto);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public async Task AddAsync_ReturnsFalse_WhenOrderDtoIsNull()
    {
        // Arrange
        var categoryDto = new CategoryDto() { Id = new Guid(),Name = "test"};

        // Act
        var result = await _service.AddAsync(categoryDto);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task AddAsync_ThrowsException_WhenRepositoryThrows()
    {      
        // Arrange
        var categoryDto = new CategoryDto() { Id = new Guid(),Name = "test"};

        _categoryRepoMock.Setup(r => r.AddAsync(It.IsAny<Category>())).ThrowsAsync(new Exception());

        // Act
        var result = await _service.AddAsync(categoryDto);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task UpdateAsync_ReturnsTrue_WhenOrderIsUpdated()
    {
        // Arrange
        var categoryDto = new CategoryDto() { Id = new Guid(),Name = "test"};
        _categoryRepoMock.Setup(r => r.ExistsAsync(categoryDto.Id)).ReturnsAsync(true);
        _categoryRepoMock.Setup(r => r.ValidateForUpdateAsync(It.IsAny<Category>())).ReturnsAsync(true);
        _categoryRepoMock.Setup(r => r.Update(It.IsAny<Category>())).ReturnsAsync(true);

        // Act
        var result = await _service.UpdateAsync(categoryDto);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateAsync_ReturnsFalse_WhenOrderNotExists()
    {
        // Arrange
        var categoryDto = new CategoryDto() { Id = new Guid(),Name = "test"};
        _categoryRepoMock.Setup(r => r.ExistsAsync(categoryDto.Id)).ReturnsAsync(false);

        // Act
        var result = await _service.UpdateAsync(categoryDto);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public async Task UpdateAsync_ReturnsFalse_WhenThereIsValidationError()
    {
        // Arrange
        var categoryDto = new CategoryDto() { Id = new Guid(),Name = "test"};
        _categoryRepoMock.Setup(r => r.ExistsAsync(categoryDto.Id)).ReturnsAsync(true);
        _categoryRepoMock.Setup(r => r.ValidateForCreateAsync(It.IsAny<Category>())).ReturnsAsync(false);

        // Act
        var result = await _service.UpdateAsync(categoryDto);

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
        var id = Guid.NewGuid();
        var category = new Category { Id = id, Name = "test" };
        _categoryRepoMock.Setup(r => r.DeleteAsync(It.IsAny<Category>())).ReturnsAsync(true);
        var categoryDto = _mapper.Map<CategoryDto>(category);

        // Act
        var result = await _service.DeleteAsync(categoryDto.Id);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteAsync_ReturnsFalse_WhenOrderNotExists()
    {
        // Arrange
        var id = new Guid();
        var category = new Category { Id = id,Name = "test"};
        _categoryRepoMock.Setup(r => r.ExistsAsync(id)).ReturnsAsync(false);
        var categoryDto = _mapper.Map<CategoryDto>(category);

        // Act
        var result = await _service.DeleteAsync(categoryDto.Id);

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
        var category = new Category { Id = id,Name = "test"};
        _categoryRepoMock.Setup(r => r.ExistsAsync(id)).ReturnsAsync(true);
        _categoryRepoMock.Setup(r => r.DeleteAsync(category)).ThrowsAsync(new Exception());
        var categoryDto = _mapper.Map<CategoryDto>(category);


        // Act
        var result = await _service.DeleteAsync(categoryDto.Id);

        // Assert
        result.Should().BeFalse();
    }
}
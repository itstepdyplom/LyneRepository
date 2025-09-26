using Lyne.Domain.Entities;
using Lyne.Infrastructure.Caching;
using Lyne.Infrastructure.Persistence;
using Lyne.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Lyne.Tests.RepositoriesTests;

public class ProductRepositoryTests : IAsyncLifetime
{
    private readonly AppDbContext _context;
    private readonly Mock<ILogger<ProductRepository>> _mockLogger;
    private readonly ProductRepository _repository;
    private readonly Mock<ICacheService> _mockCache;

    public ProductRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql("DataSource=:memory:")
            .Options;
        _context = new AppDbContext(options);
        _context.Database.OpenConnection();
        _context.Database.EnsureCreated();

        _mockLogger = new Mock<ILogger<ProductRepository>>();
        _mockCache = new Mock<ICacheService>();
        _repository = new ProductRepository(_context, _mockLogger.Object,_mockCache.Object);
    }

    public async Task InitializeAsync()
    {
        _context.ChangeTracker.Clear();
        _context.Products.RemoveRange(_context.Products);
        await _context.SaveChangesAsync();
    }

    public Task DisposeAsync() => Task.CompletedTask;

    [Fact]
    public async Task AddAsync_ShouldAddProduct_WhenValid()
    {
        // Arrange
        var category = new Category { Name = "TestCat", Description = "Desc" };
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        var product = new Product
        {
            Name = "test",
            Description = "Test Description",
            Price = 99.99m,
            StockQuantity = 10,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            CategoryId = category.Id,
            Brand = "TestBrand",
            Color = "Red",
            ImageUrl = "http://example.com/image.jpg",
            IsActive = true
        };
        // Act
        var result = await _repository.AddAsync(product);

        // Assert
        Assert.True(result);
        var saved = await _context.Products.FindAsync(product.Id);
        Assert.NotNull(saved);
    }
    

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenProductNotFound()
    {
        // Arrange
        
        // Act
        var result = await _repository.GetByIdAsync(new Guid());
        
        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnProduct_WhenExists()
    {
        // Arrange
        var category = new Category
        {
            Name = "test",
            Description = "Test Description",
        };
        
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        var product = new Product
        {
            Name = "test",
            Description = "Test Description",
            Price = 99.99m,
            StockQuantity = 10,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            CategoryId = category.Id
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetByIdAsync(product.Id);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(product.Brand, result!.Brand);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllProducts()
    {
        // Arrange
        var category = new Category
        {
            Name = "test",
            Description = "Test Description",
        };
        
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        _context.Products.AddRange(
            new Product { 
                Name = "test",
                Description = "Test Description",
                Price = 99.99m,
                StockQuantity = 10,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CategoryId = category.Id
            },
            new Product { 
                Name = "test2",
                Description = "Test Description2",
                Price = 999.99m,
                StockQuantity = 15,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CategoryId = category.Id
            }
        );
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task Update_ShouldReturnFalse_WhenProductNotFound()
    {
        // Arrange
        var fakeProduct = new Product
        {
            Id = Guid.NewGuid(), // явно задаємо Id, якого немає в БД
            Name = "Nonexistent",
            Description = "No description",
            Price = 9.99m,
            StockQuantity = 0,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            CategoryId = Guid.NewGuid() // теж неіснуючий
        };

        // Act
        var result = await _repository.Update(fakeProduct);
        
        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public async Task Update_ShouldReturnTrue_WhenProductExists()
    {
        // Arrange
        var category = new Category { Name = "Category", Description = "Desc" };
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        var product = new Product
        {
            Name = "OldName",
            Description = "Old Desc",
            Price = 10,
            StockQuantity = 5,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            CategoryId = category.Id,
            Brand = "TestBrand",
            Color = "Blue",
            ImageUrl = "http://example.com/image.jpg",
            IsActive = true
        };
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        // Modify
        product.Name = "NewName";
        
        // Act
        var result = await _repository.Update(product);

        // Assert
        Assert.True(result);
        var updated = await _context.Products.FindAsync(product.Id);
        Assert.Equal("NewName", updated!.Name);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnFalse_WhenProductNotFound()
    {
        // Arrange
        var product = new Product { 
            Name = "test",
            Description = "Test Description",
            Price = 99.99m,
            StockQuantity = 10,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow 
        };
        
        // Act
        var result = await _repository.DeleteAsync(product);
        
        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public async Task DeleteAsync_ShouldReturnTrue_WhenProductExists()
    {
        // Arrange
        var category = new Category { Name = "Cat", Description = "Desc" };
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        var product = new Product
        {
            Name = "ToDelete",
            Description = "Delete me",
            Price = 15,
            StockQuantity = 3,
            CategoryId = category.Id,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Brand = "TestBrand",
            Color = "Red",
            ImageUrl = "http://example.com/image.jpg",
            IsActive = true
        };
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        var savedProduct = await _context.Products.FirstAsync(p => p.Id == product.Id);

        // Act
        var result = await _repository.DeleteAsync(savedProduct);

        // Assert
        Assert.True(result);

        var deleted = await _context.Products.FindAsync(product.Id);
        Assert.Null(deleted);
    }

    [Fact]
    public async Task ExistsAsync_ShouldReturnFalse_WhenProductNotFound()
    {
        // Arrange

        // Act
        var exists = await _repository.ExistsAsync(new Guid());
        
        // Assert
        Assert.False(exists);
    }

    [Fact]
    public async Task ExistsAsync_ShouldReturnTrue_WhenProductExists()
    {
        // Arrange
        var category = new Category
        {
            Name = "test",
            Description = "Test Description",
        };
        
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        
        var product = new Product
        {
            Name = "test",
            Description = "Test Description",
            Price = 99.99m,
            StockQuantity = 10,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            CategoryId = category.Id
        };
        
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        // Act
        var exists = await _repository.ExistsAsync(product.Id);
        
        // Assert
        Assert.True(exists);
    }
    
    [Fact]
    public async Task ValidateForCreateAsync_ShouldReturnFalse_WhenMissingRequiredFields()
    {
        // Arrange
        var product = new Product
        {
            Name = "",
            Description = "No name",
            Price = 10,
            StockQuantity = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        // Act
        var isValid = await _repository.ValidateForCreateAsync(product);
        
        // Assert
        Assert.False(isValid);
    }

    [Fact]
    public async Task ValidateForCreateAsync_ShouldReturnFalse_WhenCategoryDoesNotExist()
    {
        // Arrange
        var product = new Product
        {
            Name = "Name",
            Description = "Desc",
            Price = 10,
            StockQuantity = 1,
            CategoryId = Guid.NewGuid(), // no such category
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        // Act
        var isValid = await _repository.ValidateForCreateAsync(product);
        
        // Assert
        Assert.False(isValid);
    }

    [Fact]
    public async Task ValidateForCreateAsync_ShouldReturnTrue_WhenValid()
    {
        // Arrange
        var category = new Category { Name = "ValidCat", Description = "Desc" };
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        var product = new Product
        {
            Name = "Valid",
            Brand = "BrandX",
            Color = "Red",
            Description = "Desc",
            ImageUrl = "image.png",
            Price = 50,
            StockQuantity = 8,
            IsActive = true,
            CategoryId = category.Id,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        // Act
        var isValid = await _repository.ValidateForCreateAsync(product);
        
        // Assert
        Assert.True(isValid);
    }
}

using Lyne.Domain.Entities;
using Lyne.Infrastructure.Persistence;
using Lyne.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace Lyne.Tests.RepositoriesTests;

public class CategoryRepositoryTests : IAsyncLifetime
{
    private readonly AppDbContext _context;
    private readonly Mock<ILogger<CategoryRepository>> _mockLogger;
    private readonly CategoryRepository _repository;

    public CategoryRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;
        _context = new AppDbContext(options);
        _context.Database.OpenConnection();
        _context.Database.EnsureCreated();

        _mockLogger = new Mock<ILogger<CategoryRepository>>();
        _repository = new CategoryRepository(_context, _mockLogger.Object);
    }

    public async Task InitializeAsync()
    {
        _context.ChangeTracker.Clear();
        _context.Categories.RemoveRange(_context.Categories);
        await _context.SaveChangesAsync();
    }
    public Task DisposeAsync() => Task.CompletedTask;

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenCategoryDoesNotExist()
    {
        // Arrange
        
        // Act
        var result = await _repository.GetByIdAsync(new Guid());
        
        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public async Task GetAllAsync_ShouldReturnAllCategories()
    {
        // Arrange
        var category1 = new Category { Name = "Cat1", Description = "Desc1" };
        var category2 = new Category { Name = "Cat2", Description = "Desc2" };
        _context.Categories.AddRange(category1, category2);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Contains(result, c => c.Name == "Cat1");
        Assert.Contains(result, c => c.Name == "Cat2");
    }

    [Fact]
    public async Task AddAsync_ShouldAddCategory_WhenValid()
    {
        // Arrange
        var category = new Category { Name = "Test", Description = "Test Desc"};
        
        // Act
        var result = await _repository.AddAsync(category);
        
        // Assert
        Assert.True(result);

        var saved = await _context.Categories.FindAsync(category.Id);
        Assert.NotNull(saved);
        Assert.Equal("Test", saved.Name);
    }

    [Fact]
    public async Task Update_ShouldReturnTrue_WhenValid()
    {
        // Arrange
        var category = new Category { Name = "Test", Description = "Test Desc" };
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        category.Name = "Updated";
        
        // Act
        var result = await _repository.Update(category);
        
        // Assert
        Assert.True(result);

        var updated = await _context.Categories.FindAsync(category.Id);
        Assert.Equal("Updated", updated.Name);
    }

    [Fact]
    public async Task Update_ShouldReturnFalse_WhenCategoryDoesNotExist()
    {
        // Arrange
        var category = new Category { Id = Guid.NewGuid(), Name = "Ghost", Description = "Ghost" };
        
        // Act
        var result = await _repository.Update(category);
        
        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnTrue_WhenCategoryExists()
    {
        // Arrange
        var category = new Category { Name = "Test", Description = "Test Desc" };
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.DeleteAsync(category);
        
        // Assert
        Assert.True(result);

        var deleted = await _context.Categories.FindAsync(category.Id);
        Assert.Null(deleted);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnFalse_WhenCategoryDoesNotExist()
    {
        // Arrange
        var category = new Category { Id = Guid.NewGuid(), Name = "Ghost", Description = "Ghost" };
        
        // Act
        var result = await _repository.DeleteAsync(category);
        
        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task ExistsAsync_ShouldReturnTrue_WhenCategoryExists()
    {
        // Arrange
        var category = new Category { Name = "Test", Description = "Test Desc" };
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        // Act
        var exists = await _repository.ExistsAsync(category.Id);
        
        // Assert
        Assert.True(exists);
    }

    [Fact]
    public async Task ExistsAsync_ShouldReturnFalse_WhenCategoryDoesNotExist()
    {
        // Arrange
        
        // Act
        var exists = await _repository.ExistsAsync(Guid.NewGuid());
        
        // Assert
        Assert.False(exists);
    }

    [Fact]
    public async Task ValidateForCreateAsync_ShouldReturnFalse_WhenNameMissing()
    {
        // Arrange
        var category = new Category { Name = "", Description = "Desc" };
        
        // Act
        var result = await _repository.ValidateForCreateAsync(category);
        
        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task ValidateForCreateAsync_ShouldReturnTrue_WhenValid()
    {
        // Arrange
        var category = new Category { Name = "Valid", Description = "Desc" };
        
        // Act
        var result = await _repository.ValidateForCreateAsync(category);
        
        // Assert
        Assert.True(result);
    }
}
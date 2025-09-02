using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.Infrastructure.Internal;

namespace Lyne.Tests.OtherTests;

public class DbContextOptionsBuilderExtensionsTests
{
    [Fact]
    public void ConfigureDbContext_SetsExpectedOptions()
    {
        var optionsBuilder = new DbContextOptionsBuilder();
        
        // Call your extension/configuration method
        optionsBuilder.UseInMemoryDatabase("TestDb");

        // Assert that the provider is set
        var extension = optionsBuilder.Options.FindExtension<InMemoryOptionsExtension>();
        Assert.NotNull(extension);
        Assert.Equal("TestDb", extension.StoreName);
    }
}
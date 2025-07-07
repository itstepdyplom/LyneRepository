using FluentAssertions;
using Lyne.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Lyne.Tests.OtherTests;

public class AppDbContextTests
{
    [Fact]
    public void Can_Construct_AppDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite("TestDb")
            .Options;
        var context = new AppDbContext(options);
        context.Should().NotBeNull();
    }

    [Fact]
    public void DbSets_Are_Accessible()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite("TestDb2")
            .Options;
        var context = new AppDbContext(options);
        context.Products.Should().NotBeNull();
        context.Categories.Should().NotBeNull();
        context.Orders.Should().NotBeNull();
        context.Users.Should().NotBeNull();
        context.Addresses.Should().NotBeNull();
    }
}

using FluentAssertions;
using Lyne.Domain.Entities;

namespace Lyne.Tests.DtosEntitiesTests;

public class OrderEntityPropertyTests
{
    [Fact]
    public void CreatedAt_SetAndGet_Works()
    {
        var order = new Order(){ Id = 1};
        var now = DateTime.UtcNow;
        order.CreatedAt = now;
        order.CreatedAt.Should().Be(now);
    }

    [Fact]
    public void Products_SetAndGet_Works()
    {
        var products = new List<Product>
        {
            new Product { Id = Guid.NewGuid(), Name = "P1" }
        };
        var order = new Order(){Id = 1};
        order.Products = products;
        order.Products.Should().BeEquivalentTo(products);
    }

    [Fact]
    public void User_SetAndGet_Works()
    {
        var user = new User { Id = 1, Name = "Test", ForName = "Test", PasswordHash = "test", Email = "test@example.com", Genre = "test",Role = "User"};
        var order = new Order(){Id = 1};
        order.User = user;
        order.User.Should().Be(user);
    }

    [Fact]
    public void ShippingAddress_SetAndGet_Works()
    {
        var address = new Address { Id = 1, Street = "Main", City = "City", State = "State", Country = "Country" };
        var order = new Order(){Id = 1};
        order.ShippingAddress = address;
        order.ShippingAddress.Should().Be(address);
    }
}

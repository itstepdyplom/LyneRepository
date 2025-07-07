using Lyne.Domain.Entities;
using Lyne.Domain.IRepositories;
using Lyne.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lyne.Infrastructure.Repositories;

public class OrderRepository(AppDbContext context, ILogger<OrderRepository> logger): IOrderRepository
{
    public async Task<List<Order>> GetAllAsync()
    {
        var orders = await context.Orders.ToListAsync();
        return orders;
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        var address = await context.Orders.FirstOrDefaultAsync(x => x.Id == id);
        logger.LogInformation("Address id:{Id} users", address!.Id);
        return await Task.FromResult(address);
    }

    public async Task<bool> AddAsync(Order? order)
    {
        if (order == null)
        {
            logger.LogInformation("Cannot add order with id:{Id}, some fields are empty", order!.Id);
            return false;
        }
        await context.Orders.AddAsync(order);
        logger.LogInformation("Order with id:{Id} added", order!.Id);
        return await Task.FromResult(true);
    }

    public async Task<bool> Update(Order? order)
    {
        if (order == null)
        {
            logger.LogInformation("Cannot update order with id:{Id}, some fields are empty", order!.Id);
            return await Task.FromResult(false);
        }
        context.Orders.Update(order);
        logger.LogInformation("Order with id:{Id} updated", order!.Id);
        return await Task.FromResult(true);
    }

    public async Task<bool> DeleteAsync(Order? order)
    {
        if (!ValidateForUpdateAsync(order).Result)
        {
            logger.LogInformation("Cannot update order with id:{Id}, some fields are empty", order!.Id);
            return await Task.FromResult(false);
        }
        var result = context.Orders.Remove(order);
        if (result.State == EntityState.Detached)
        {
            logger.LogInformation("Order with id:{Id} deleted", order!.Id);
            return true;
        }
        else
        {
            logger.LogInformation("Cannot delete order with id:{Id}", order!.Id);
            return false;
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        var order = await context.Orders.FindAsync(id);
        logger.LogInformation("Getting order with id:{Id}", id);
        return order != null ? true : false;
    }

    public Task<bool> ValidateForCreateAsync(Order order)
    {
        return Task.FromResult(
            order.OrderStatus != default &&
            !string.IsNullOrEmpty(order.PaymentMethod) &&
            order.ShippingAddressId != default &&
            !string.IsNullOrEmpty(order.TrackingNumber.ToString()) &&
            order.UserId != default
        );
    }

    public async Task<bool> ValidateForUpdateAsync(Order? order)
    {
        if (order == null)
            return false;

        var orderExists = await context.Orders.AnyAsync(u => u.Id == order.Id);

        return order.OrderStatus != default &&
               !string.IsNullOrEmpty(order.PaymentMethod) &&
               order.ShippingAddressId != default &&
               !string.IsNullOrEmpty(order.TrackingNumber.ToString()) &&
               order.UserId != default &&
               orderExists;
    }
}

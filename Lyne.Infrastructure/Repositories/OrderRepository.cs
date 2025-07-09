using AutoMapper;
using Lyne.Domain.Entities;
using Lyne.Domain.Enums;
using Lyne.Domain.IRepositories;
using Lyne.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lyne.Infrastructure.Repositories;

public class OrderRepository(AppDbContext context, ILogger<OrderRepository> logger, IMapper mapper): IOrderRepository
{
    public async Task<List<Order>> GetAllAsync()
    {
        var orders = await context.Orders.ToListAsync();
        return orders;
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        var order = await context.Orders.FindAsync(id);
        if (order == null)
        {
            logger.LogInformation("Order with id:{Id} not found", id);
            return null;
        }
        logger.LogInformation("Order with id:{Id} found", order.Id);
        return order;
    }

    public async Task<bool> AddAsync(Order? order)
    {
        if (order == null)
        {
            logger.LogInformation("Cannot add order with id:{Id}, some fields are empty", order!.Id);
            return false;
        }
        await context.Orders.AddAsync(order);
        await context.SaveChangesAsync();
        logger.LogInformation("Order with id:{Id} added", order!.Id);
        return await Task.FromResult(true);
    }

    public async Task<bool> Update(Order? order)
    {
        if (order == null)
        {
            logger.LogInformation("Cannot update order with id:{Id}, some fields are empty", 0);
            return false;
        }

        var existingOrder = await context.Orders.FindAsync(order.Id);
        if (existingOrder == null)
            return false;
        
        //context.Entry(existingOrder).CurrentValues.SetValues(order);
        mapper.Map(order, existingOrder);

        if (!await ValidateForUpdateAsync(existingOrder))
        {
            logger.LogInformation("Cannot update order with id:{Id}, validation issues", existingOrder!.Id);
            return false;
        }
        await context.SaveChangesAsync();
        logger.LogInformation("Order with id:{Id} updated", existingOrder.Id);
        return true;
    }

    public async Task<bool> DeleteAsync(Order? order)
    {
        var existingOrder = await context.Orders.FindAsync(order.Id);
        if (existingOrder == null)
            return false;

        context.Orders.Remove(existingOrder);
        await context.SaveChangesAsync();
        return true;
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
            order.TrackingNumber != 0 &&
            order.UserId != default
        );
    }

    public async Task<bool> ValidateForUpdateAsync(Order? order)
    {
        if (order == null)
            return false;

        var orderExists = await context.Orders.AnyAsync(u => u.Id == order.Id);
        logger.LogInformation("Order Exists: {Exists} for Id: {Id}", orderExists, order.Id);

        return order.OrderStatus != OrderStatus.Unknown &&
               !string.IsNullOrEmpty(order.PaymentMethod) &&
               order.ShippingAddressId != default &&
               !string.IsNullOrEmpty(order.TrackingNumber.ToString()) &&
               order.UserId != default &&
               orderExists;
    }
}

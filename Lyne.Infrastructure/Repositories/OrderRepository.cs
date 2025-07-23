using AutoMapper;
using Lyne.Domain.Entities;
using Lyne.Domain.Enums;
using Lyne.Domain.IRepositories;
using Lyne.Infrastructure.Caching;
using Lyne.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lyne.Infrastructure.Repositories;

public class OrderRepository(AppDbContext context, ILogger<OrderRepository> logger, IMapper mapper, ICacheService cacheService): IOrderRepository
{
    public async Task<List<Order>> GetAllAsync()
    {
        var cached = await cacheService.GetAllAsync<Order>("order");
        if (cached != null && cached.Count > 0)
            return cached;
        var orders = await context.Orders.ToListAsync();
        return orders;
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        var cacheKey = $"order:{id}";
        var order = await cacheService.GetAsync<Order>(cacheKey);
        if (order is null)
        {
            order = await context.Orders.FindAsync(id);
            if (order is not null)
            {
                logger.LogInformation("Order with id:{Id} found", id);
                await cacheService.SetAsync(cacheKey, order, "order", TimeSpan.FromMinutes(15));
            }
            else
            {
                logger.LogInformation("Order with id:{Id} not found", id);
                return null;
            }
        }
        return order;
    }

    public async Task<bool> AddAsync(Order? order)
    {
        if (order is null)
        {
            logger.LogInformation("Cannot add order with id:{Id}, some fields are empty", order!.Id);
            return false;
        }
        if (!await ValidateForCreateAsync(order))
        {
            logger.LogInformation("Cannot add order with id:{Id}, validation issues", order!.Id);
            return false;
        }
        var cacheKey = $"order:{order.Id}";
        await cacheService.SetAsync(cacheKey, order, "order", TimeSpan.FromMinutes(15));
        await context.Orders.AddAsync(order);
        await context.SaveChangesAsync();
        logger.LogInformation("Order with id:{Id} added", order!.Id);
        return true;
    }

    public async Task<bool> Update(Order? order)
    {
        if (order is null)
        {
            logger.LogInformation("Cannot update order with id:{Id}, some fields are empty", 0);
            return false;
        }

        if (!await ExistsAsync(order.Id))
        {
            logger.LogInformation("Order with id:{Id} not found", order.Id);
            return false;
        }
        
        if (!await ValidateForUpdateAsync(order))
        {
            logger.LogInformation("Cannot update order with id:{Id}, validation issues", order!.Id);
            return false;
        }
        
        var cacheKey = $"address:{order.Id}";
        await cacheService.SetAsync(cacheKey, order, "order", TimeSpan.FromMinutes(15));
        context.Orders.Update(order);
        await context.SaveChangesAsync();
        logger.LogInformation("Order with id:{Id} updated", order.Id);
        return true;
    }

    public async Task<bool> DeleteAsync(Order? order)
    {
        if (order is null)
        {
            logger.LogInformation("Cannot delete order: order is null");
            return false;
        }

        if (!await ExistsAsync(order.Id))
        {
            logger.LogInformation("Order with id:{Id} not found", order!.Id);
            return false;
        }

        var cacheKey = $"order:{order.Id}";
        await cacheService.RemoveAsync(cacheKey,"order"); 
        context.Orders.Remove(order);
        await context.SaveChangesAsync();
        logger.LogInformation("Order with id:{Id} deleted", order!.Id);
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
        bool isValid =
            order.OrderStatus != default &&
            !string.IsNullOrEmpty(order.PaymentMethod) &&
            order.ShippingAddressId != default &&
            order.TrackingNumber != 0 &&
            order.UserId != default;

        logger.LogInformation("ValidateForCreateOrderAsync: Validation {Result}", isValid ? "passed" : "failed");
        return Task.FromResult(isValid);
    }

    public async Task<bool> ValidateForUpdateAsync(Order? order)
    {
        if (order is null)
            return false;

        var orderExists = await context.Orders.AnyAsync(u => u.Id == order.Id);
        logger.LogInformation("Order Exists: {Exists} for Id: {Id}", orderExists, order.Id);

        bool isValid = order.OrderStatus != OrderStatus.Unknown &&
                       !string.IsNullOrEmpty(order.PaymentMethod) &&
                       order.ShippingAddressId != default &&
                       !string.IsNullOrEmpty(order.TrackingNumber.ToString()) &&
                       order.UserId != default &&
                       orderExists;
        logger.LogInformation("ValidateForUpdateOrderAsync: Validation {Result}", isValid ? "passed" : "failed");
        return isValid;
    }
}

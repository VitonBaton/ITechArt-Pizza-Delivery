﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Interfaces;
using ITechArtPizzaDelivery.Domain.Models;
using ITechArtPizzaDelivery.Infrastructure.Contexts;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;

namespace ITechArtPizzaDelivery.Infrastructure.Repositories.EFRepositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly PizzaDeliveryContext _dbContext;

        public OrdersRepository(PizzaDeliveryContext context)
        {
            _dbContext = context;
        }

        public async Task<Order> CreateNewOrder(int customerId, string address, int deliveryId, int paymentId, string comment)
        {
            var customer = await _dbContext.Users
                .Include(u=>u.Cart)
                .ThenInclude(c => c.Pizzas)
                .FirstAsync(u => u.Id == customerId);

            if (customer is null)
            {
                throw new KeyNotFoundException("Customer with that id not found");
            }

            if (customer.Cart is null)
            {
                throw new KeyNotFoundException("Customer has an empty cart");
            }

            var delivery = await _dbContext.Deliveries
                .FindAsync(deliveryId);
            if (delivery is null)
            {
                throw new KeyNotFoundException("Delivery with that id not found");
            }

            var payment = await _dbContext.Payments
                .FindAsync(paymentId);
            if (payment is null)
            {
                throw new KeyNotFoundException("Payment with that id not found");
            }

            var price = customer.Cart.Pizzas?
                .Sum(p => p.Price);

            var order = new Order()
            {
                Customer = customer,
                Cart = customer.Cart,
                Address = address,
                Delivery = delivery,
                Payment = payment,
                Comment = comment,
                CreateAt = DateTime.Now,
                Price = price.Value,
                Status = Order.StatusType.New
            };
            await _dbContext.Orders.AddAsync(order);
            customer.Cart = null;
            customer.CartId = null;
            await _dbContext.SaveChangesAsync();
            return order;
        }

        public async Task CancelOrder(int orderId)
        {
            var order = await _dbContext.Orders
                .FindAsync(orderId);
            if (order is null)
            {
                throw new KeyNotFoundException("Order with that id not found");
            }

            order.Status = Order.StatusType.Canceled;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Order>> GetCustomerOrders(int customerId)
        {
            var customer = await _dbContext.Users
                .Include(u => u.Orders)
                    .ThenInclude(o => o.Delivery)
                .Include(u => u.Orders)
                    .ThenInclude(o => o.Payment)
                .Include(u => u.Orders)
                    .ThenInclude(o => o.Promocode)
                .Include(u => u.Orders)
                    .ThenInclude(o => o.Cart)
                        .ThenInclude(c => c.Pizzas)
                .FirstAsync(u => u.Id == customerId);

            if (customer is null)
            {
                throw new KeyNotFoundException("Customer with that id not found");
            }

            return customer.Orders;
        }

        public async Task AddPromocodeToOrder(int orderId, string promocodeName)
        {
            var order = await _dbContext.Orders
                .Include(o => o.Promocode)
                .FirstAsync(o => o.Id == orderId);

            if (order is null)
            {
                throw new KeyNotFoundException("Order with that id not found");
            }

            if (order.Promocode is not null)
            {
                throw new Exception("Order already has a promocode");
            }

            var promocode = await _dbContext.Promocodes
                .FirstAsync(p => p.Name.Equals(promocodeName));

            if (promocode is null)
            {
                throw new KeyNotFoundException("Can't find that promocode");
            }

            order.Promocode = promocode;
            order.Price -= promocode.Discount;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllUsersAndOrders()
        {
            var users = await _dbContext.Users
                .Include(u => u.Orders)
                    .ThenInclude(o => o.Delivery)
                .Include(u => u.Orders)
                    .ThenInclude(o => o.Payment)
                .ToListAsync();

            return users;
        }
    }
}
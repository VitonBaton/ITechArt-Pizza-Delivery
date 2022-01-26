using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Errors;
using ITechArtPizzaDelivery.Domain.Interfaces;
using ITechArtPizzaDelivery.Domain.Models;
using ITechArtPizzaDelivery.Domain.Pagination;
using ITechArtPizzaDelivery.Infrastructure.Contexts;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;

namespace ITechArtPizzaDelivery.Infrastructure.Repositories.EFRepositories
{
    public class OrdersRepository : GenericRepository<Order>, IOrdersRepository
    {
        public OrdersRepository(PizzaDeliveryContext context) : base(context)
        {
        }

        public async Task<Order> CreateNewOrder(int customerId, string address, int deliveryId, int paymentId, string comment)
        {
            var customer = await _dbContext.Users
                .Include(u=>u.Cart)
                .ThenInclude(c => c.Pizzas)
                .FirstOrDefaultAsync(u => u.Id == customerId);

            if (customer is null)
            {
                throw new KeyNotFoundException("Customer not found");
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

            if (price is null)
            {
                throw new ServerErrorException("Can't calculate price");
            }
            
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

        public async Task<PagedList<Order>> GetCustomerOrders(int customerId, PagingParameters parameters)
        {
            var customer = await _dbContext.Users
                .FindAsync(customerId);
            if (customer is null)
            {
                throw new KeyNotFoundException("Customer with that id not found");
            }

            var orders = _dbContext.Entry(customer)
                .Collection(u => u.Orders)
                .Query()
                .Include(o => o.Delivery)
                .Include(o => o.Payment)
                .Include(o => o.Promocode)
                .Include(o => o.Cart)
                    .ThenInclude(c => c.Pizzas);
            
            return await PagedList<Order>.CreateAsync(
                orders.AsQueryable(),
                parameters.PageNumber,
                parameters.PageSize);
        }

        public async Task<Order> GetOrderOfCustomer(int customerId, int orderId)
        {
            var order = await _dbContext.Orders
                .Where(o => o.CustomerId == customerId && o.Id == orderId)
                .Include(o => o.Promocode)
                .Include(o => o.Delivery)
                .Include(o => o.Payment)
                .Include(o => o.Cart)
                    .ThenInclude(c => c.Pizzas)
                .FirstOrDefaultAsync();

            if (order is null)
            {
                throw new KeyNotFoundException("Order not found");
            }
            return order;
        }

        public async Task AddPromocodeToOrder(int orderId, string promocodeName)
        {
            var order = await _dbContext.Orders
                .Include(o => o.Promocode)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order is null)
            {
                throw new KeyNotFoundException("Order with that id not found");
            }

            if (order.Promocode is not null)
            {
                throw new BadRequestException("Order already has a promocode");
            }

            var promocode = await _dbContext.Promocodes
                .FirstOrDefaultAsync(p => p.Name.Equals(promocodeName));

            if (promocode is null)
            {
                throw new KeyNotFoundException("Can't find that promocode");
            }

            order.Promocode = promocode;
            order.Price -= promocode.Discount;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<PagedList<User>> GetAllUsersAndOrders(PagingParameters parameters)
        {
            var usersQuery = _dbContext.Users
                .Include(u => u.Orders)
                .ThenInclude(o => o.Delivery)
                .Include(u => u.Orders)
                .ThenInclude(o => o.Payment)
                .AsQueryable();

            return await PagedList<User>.CreateAsync(usersQuery, parameters.PageNumber, parameters.PageSize);
        }
    }
}

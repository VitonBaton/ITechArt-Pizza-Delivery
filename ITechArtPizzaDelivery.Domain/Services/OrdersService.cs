using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Errors;
using ITechArtPizzaDelivery.Domain.Interfaces;
using ITechArtPizzaDelivery.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ITechArtPizzaDelivery.Domain.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IPromocodesRepository _promocodesRepository;
        private readonly UserManager<User> _userManager;
        private readonly IGenericRepository<Delivery> _deliveriesRepository;
        private readonly IGenericRepository<Payment> _paymentsRepository;

        public OrdersService(IOrdersRepository ordersRepository,
            IPromocodesRepository promocodesRepository,
            UserManager<User> userManager,
            IGenericRepository<Delivery> deliveriesRepository,
            IGenericRepository<Payment> paymentsRepository)
        {
            _ordersRepository = ordersRepository;
            _promocodesRepository = promocodesRepository;
            _userManager = userManager;
            _deliveriesRepository = deliveriesRepository;
            _paymentsRepository = paymentsRepository;
        }

        public async Task<Order> CreateNewOrder(int customerId, Order order)
        {
            var customer = await _userManager.GetUserById(customerId);

            if (customer is null)
            {
                throw new KeyNotFoundException("Customer not found");
            }

            if (customer.Cart is null)
            {
                throw new KeyNotFoundException("Customer has an empty cart");
            }

            var delivery = await _deliveriesRepository.GetById(order.DeliveryId);
            if (delivery is null)
            {
                throw new KeyNotFoundException("Delivery not found");
            }

            var payment = await _paymentsRepository.GetById(order.PaymentId);
            if (payment is null)
            {
                throw new KeyNotFoundException("Payment not found");
            }

            var price = customer.Cart.CartPizzas?
                .Sum(cp => cp.Count * cp.Pizza.Price);

            if (price is null)
            {
                throw new ServerErrorException("Can't calculate price");
            }
            
            order.Delivery = delivery;
            order.Payment = payment;
            order.CreateAt = DateTime.Now;
            order.Status = Order.StatusType.New;
            order.Customer = customer;
            order.Cart = customer.Cart;
            order.Price = price.Value;
            await _ordersRepository.Insert(order);
            customer.Cart = null;
            customer.CartId = null;
            
            var result = await _userManager.UpdateAsync(customer);
            if (!result.Succeeded)
            {
                throw new ServerErrorException("Can't save customer changes");
            }
            return order;
        }

        public async Task CancelOrder(int customerId, int orderId)
        {
            var order = await _ordersRepository.GetOrderOfCustomer(customerId,orderId);
            if (order is null)
            {
                throw new KeyNotFoundException("Order not found");
            }
            
            if (order.Status is not (Order.StatusType.New or Order.StatusType.Payed))
            {
                throw new BadRequestException("Can't cancel this order");
            }

            order.Status = Order.StatusType.Canceled;
            await _ordersRepository.Update(order);
        }

        public async Task DeliverOrder(int customerId, int orderId)
        {
            var order = await _ordersRepository.GetOrderOfCustomer(customerId,orderId);
            if (order is null)
            {
                throw new KeyNotFoundException("Order not found");
            }
            
            if (order.Status is not Order.StatusType.Payed)
            {
                throw new BadRequestException("Can't deliver this order");
            }

            order.Status = Order.StatusType.Delivered;
            await _ordersRepository.Update(order);
        }
        
        public async Task PayOrder(int customerId, int orderId)
        {
            var order = await _ordersRepository.GetOrderOfCustomer(customerId, orderId);
            if (order is null)
            {
                throw new KeyNotFoundException("Order not found");
            }
            
            if (order.Status is not Order.StatusType.New)
            {
                throw new BadRequestException("Can't pay this order");
            }

            order.Status = Order.StatusType.Payed;
            await _ordersRepository.Update(order);
        }

        public async Task<List<Order>> GetCustomerOrders(int customerId)
        {
            return await _ordersRepository.GetCustomerOrders(customerId);
        }

        public async Task<decimal> AddPromocodeToOrder(int customerId, int orderId, string promocodeName)
        {
            var order = await _ordersRepository.GetOrderOfCustomer(customerId, orderId);

            Console.WriteLine(orderId);
            
            if (order.Status is Order.StatusType.Canceled)
            {
                throw new BadRequestException("Order canceled");
            }
            
            if (order.Promocode is not null)
            {
                throw new BadRequestException("Order already has a promocode");
            }

            var promocode = await _promocodesRepository.GetPromocodeByName(promocodeName);
            order = order.AddPromocode(promocode);
            await _ordersRepository.Update(order);
            return order.Price;
        }

        public async Task<decimal> RemovePromocodeFromOrder(int customerId, int orderId)
        {
            var order = await _ordersRepository.GetOrderOfCustomer(customerId, orderId);
            
            if (order.Status is Order.StatusType.Canceled)
            {
                throw new BadRequestException("Order canceled");
            }
            
            if (order.Promocode is null)
            {
                throw new BadRequestException("Order doesn't have a promocode");
            }

            order = order.RemovePromocode();
            await _ordersRepository.Update(order);
            return order.Price;
        }
        
        public async Task<List<User>> GetAllUsersAndOrders()
        {
            return await _ordersRepository.GetAllUsersAndOrders();
        }
    }

    public static class OrderExtension
    {
        public static Order AddPromocode(this Order order, Promocode promocode)
        {
            order.Promocode = promocode;
            order.Price *= (100 - promocode.Discount) / 100;
            return order;
        }

        public static Order RemovePromocode(this Order order)
        {
            var promocode = order.Promocode;
            if (promocode is null)
            {
                throw new ArgumentNullException(nameof(order),"Promocode in order is null");
            }
            order.Promocode = null;
            order.Price /= (100 - promocode.Discount) / 100;
            return order;
        }
    }

    public static class UserManagerExtension
    {
        public static async Task<User> GetUserById(this UserManager<User> userManager, int userId)
        {
            var user = await userManager.Users
                .Include(u=>u.Cart)
                .ThenInclude(c => c.Pizzas)
                .FirstOrDefaultAsync(u => u.Id == userId);
            return user;
        }
    }
}
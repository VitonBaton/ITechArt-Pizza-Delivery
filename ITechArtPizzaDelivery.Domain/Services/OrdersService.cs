using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Interfaces;
using ITechArtPizzaDelivery.Domain.Models;

namespace ITechArtPizzaDelivery.Domain.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository _ordersRepository;

        public OrdersService(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<Order> CreateNewOrder(int customerId, string address, int deliveryId, int paymentId, string comment)
        {
            return await _ordersRepository.CreateNewOrder(customerId, address, deliveryId, paymentId, comment);
        }

        public async Task CancelOrder(int orderId)
        {
            await _ordersRepository.CancelOrder(orderId);
        }

        public async Task<List<Order>> GetCustomerOrders(int customerId)
        {
            return await _ordersRepository.GetCustomerOrders(customerId);
        }

        public async Task AddPromocodeToOrder(int orderId, string promocodeName)
        {
            await _ordersRepository.AddPromocodeToOrder(orderId, promocodeName);
        }

        public async Task<List<User>> GetAllUsersAndOrders()
        {
            return await _ordersRepository.GetAllUsersAndOrders();
        }
    }
}
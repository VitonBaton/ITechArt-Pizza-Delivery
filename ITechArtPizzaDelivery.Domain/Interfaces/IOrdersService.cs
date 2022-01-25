using System.Collections.Generic;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Models;

namespace ITechArtPizzaDelivery.Domain.Interfaces
{
    public interface IOrdersService
    {
        Task<Order> CreateNewOrder(int customerId, Order order);
        Task CancelOrder(int customerId, int orderId);
        Task DeliverOrder(int customerId, int orderId);
        Task PayOrder(int customerId, int orderId);
        Task<List<Order>> GetCustomerOrders(int customerId);
        Task<decimal> AddPromocodeToOrder(int customerId, int orderId, string promocodeName);
        Task<decimal> RemovePromocodeFromOrder(int customerId, int orderId);
        Task<List<User>> GetAllUsersAndOrders();
    }
}
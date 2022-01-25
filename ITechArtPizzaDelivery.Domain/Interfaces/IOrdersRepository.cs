using System.Collections.Generic;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Models;

namespace ITechArtPizzaDelivery.Domain.Interfaces
{
    public interface IOrdersRepository : IGenericRepository<Order>
    {
        //Task<Order> CreateNewOrder(int customerId, string address, int deliveryId, int paymentId, string comment);
        //Task CancelOrder(int orderId);
        Task<List<Order>> GetCustomerOrders(int customerId);
        Task<Order> GetOrderOfCustomer(int customerId, int orderId);
        //Task AddPromocodeToOrder(int orderId, string promocodeName);
        Task<List<User>> GetAllUsersAndOrders();
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Models;
using ITechArtPizzaDelivery.Domain.Pagination;

namespace ITechArtPizzaDelivery.Domain.Interfaces
{
    public interface IOrdersService
    {
        Task<Order> CreateNewOrder(int customerId, Order order);
        Task CancelOrder(int customerId, int orderId);
        Task DeliverOrder(int customerId, int orderId);
        Task PayOrder(int customerId, int orderId);
        Task<PagedList<Order>> GetCustomerOrders(int customerId, PagingParameters parameters);
        Task<decimal> AddPromocodeToOrder(int customerId, int orderId, string promocodeName);
        Task<decimal> RemovePromocodeFromOrder(int customerId, int orderId);
        Task<PagedList<User>> GetAllUsersAndOrders(PagingParameters parameters);
    }
}
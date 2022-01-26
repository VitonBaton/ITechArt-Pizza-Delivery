using System.Collections.Generic;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Models;
using ITechArtPizzaDelivery.Domain.Pagination;

namespace ITechArtPizzaDelivery.Domain.Interfaces
{
    public interface IOrdersRepository : IGenericRepository<Order>
    {
        Task<PagedList<Order>> GetCustomerOrders(int customerId, PagingParameters parameters);
        Task<Order> GetOrderOfCustomer(int customerId, int orderId);
        Task<PagedList<User>> GetAllUsersAndOrders(PagingParameters parameters);
    }
}
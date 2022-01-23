using System.Collections.Generic;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Models;

namespace ITechArtPizzaDelivery.Domain.Interfaces
{
    public interface ICartRepository
    {
        Task<List<CartPizza>> AddPizzaToCart(int pizzaId, int customerId, int pizzasCount);
        Task<List<CartPizza>> GetPizzasFromCart(int customerId);
        Task Update(CartPizza cartPizza);
        Task DeleteByPizzaId(int customerId, int pizzaId);
    }
}
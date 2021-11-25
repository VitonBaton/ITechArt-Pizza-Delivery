using System.Collections.Generic;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Models;

namespace ITechArtPizzaDelivery.Domain.Interfaces
{
    public interface ICartRepository
    {
        Task<List<CartPizza>> AddPizzaToCart(int pizzaId, int customerId, int pizzasCount);
        Task<List<CartPizza>> GetPizzasFromCart(int customerId);
        Task ChangeAmountOfPizza(int pizzaId, int customerId, int pizzasCount);
        Task DeleteByPizzaId(int customerId, int pizzaId);
    }
}
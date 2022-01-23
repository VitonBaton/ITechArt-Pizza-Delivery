using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Interfaces;
using ITechArtPizzaDelivery.Domain.Models;

namespace ITechArtPizzaDelivery.Domain.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<List<CartPizza>> AddPizzaToCart(int pizzaId, int customerId, int pizzasCount)
        {
            return await _cartRepository.AddPizzaToCart(pizzaId, customerId, pizzasCount);
        }

        public async Task<List<CartPizza>> GetPizzasFromCart(int customerId)
        {
            return await _cartRepository.GetPizzasFromCart(customerId);
        }

        public async Task ChangeAmountOfPizza(int pizzaId, int customerId, int pizzasCount)
        {
            var userPizzas = await _cartRepository.GetPizzasFromCart(customerId);

            var cartPizza = userPizzas.Find(cp => cp.PizzaId == pizzaId);
            if (cartPizza is null)
            {
                throw new KeyNotFoundException("There is no that pizza in the cart");
            }

            cartPizza.Count = pizzasCount;
            await _cartRepository.Update(cartPizza);
        }

        public async Task DeleteByPizzaId(int customerId, int pizzaId)
        {
            await _cartRepository.DeleteByPizzaId(customerId, pizzaId);
        }
    }
}

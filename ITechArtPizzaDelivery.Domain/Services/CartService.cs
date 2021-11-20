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
            await _cartRepository.ChangeAmountOfPizza(pizzaId, customerId, pizzasCount);
        }

        public async Task DeleteByPizzaId(int customerId, int pizzaId)
        {
            await _cartRepository.DeleteByPizzaId(customerId, pizzaId);
        }
    }
}

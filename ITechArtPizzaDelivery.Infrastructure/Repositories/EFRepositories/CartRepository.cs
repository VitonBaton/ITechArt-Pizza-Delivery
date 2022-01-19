using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Interfaces;
using ITechArtPizzaDelivery.Domain.Models;
using ITechArtPizzaDelivery.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ITechArtPizzaDelivery.Infrastructure.Repositories.EFRepositories
{
    public class CartRepository : ICartRepository
    {
        private readonly PizzaDeliveryContext _dbContext;

        public CartRepository(PizzaDeliveryContext context)
        {
            _dbContext = context;
        }

        public async Task<List<CartPizza>> AddPizzaToCart(int pizzaId, int customerId, int pizzasCount)
        {
            var user = await _dbContext.Users                                    
                        .Include(u=> u.Cart)
                        .ThenInclude(c=>c.Pizzas)
                        .FirstOrDefaultAsync(u=> u.Id == customerId);
            if (user is null)
            {
                throw new KeyNotFoundException("User with that id not found");
            }

            user.Cart ??= new Cart
            {
                Pizzas = new List<Pizza>(),
                CartPizzas = new List<CartPizza>()
            };

            var cartPizza = new CartPizza
            {
                Cart = user.Cart,
                PizzaId = pizzaId,
                Count = pizzasCount
            };
            user.Cart.CartPizzas?.Add(cartPizza);

            await _dbContext.SaveChangesAsync();
            return user.Cart.CartPizzas;
        }

        public async Task<List<CartPizza>> GetPizzasFromCart(int userId)
        {
            // after adding authorization method will return pizzas that in cart of current user
            var user = await _dbContext.Users
                .Include(u => u.Cart)
                .ThenInclude(c=>c.Pizzas)
                .FirstOrDefaultAsync(u => u.Id == userId);
            if (user is null)
            {
                throw new KeyNotFoundException("User not found");
            }

            if (user.Cart is null)
            {
                return new List<CartPizza>();
            }

            return user.Cart.CartPizzas;
        }

        public async Task ChangeAmountOfPizza(int pizzaId, int customerId, int pizzasCount)
        {
            var user = await _dbContext.Users
                .Include(u => u.Cart)
                .ThenInclude(c=>c.CartPizzas)
                .FirstOrDefaultAsync(u => u.Id == customerId);
            if (user is null)
            {
                throw new KeyNotFoundException("User not found");
            }

            var cartPizza = user.Cart?.CartPizzas?.Find(cp => cp.PizzaId == pizzaId);
            if (user.Cart is null || cartPizza is null)
            {
                throw new KeyNotFoundException("There is no that pizza in the cart");
            }

            cartPizza.Count = pizzasCount;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteByPizzaId(int customerId, int pizzaId)
        {
            var user = await _dbContext.Users
                .Include(u => u.Cart)
                .ThenInclude(c => c.CartPizzas)
                .FirstOrDefaultAsync(u => u.Id == customerId);
            if (user is null)
            {
                throw new KeyNotFoundException("User not found");
            }

            var cartPizza = user.Cart?.CartPizzas?.Find(cp => cp.PizzaId == pizzaId);
            if (user.Cart is null || cartPizza is null)
            {
                throw new KeyNotFoundException("There is no that pizza in the cart");
            }
            _dbContext.Remove(cartPizza);
            await _dbContext.SaveChangesAsync();
        }
    }
}

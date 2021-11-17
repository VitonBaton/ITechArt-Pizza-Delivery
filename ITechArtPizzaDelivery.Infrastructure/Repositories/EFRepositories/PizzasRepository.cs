using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Interfaces;
using ITechArtPizzaDelivery.Domain.Models;
using ITechArtPizzaDelivery.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ITechArtPizzaDelivery.Infrastructure.Repositories
{
    public class PizzasRepository : IPizzasRepository
    {
        private readonly PizzaDeliveryContext _dbContext;

        public PizzasRepository(PizzaDeliveryContext context)
        {
            _dbContext = context;
        }

        async Task<Pizza> IPizzasRepository.GetById(int id)
        {
            return await _dbContext.Pizzas
                .Include(p => p.Ingredients)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Pizza> Post(Pizza pizza, int[] ingredientsId)
        {
            var ingredients = _dbContext.Ingredients
                                                    .Where(i => ingredientsId.Contains(i.Id));
            pizza.Ingredients = new List<Ingredient>();
            pizza.Ingredients.AddRange(ingredients);

            _dbContext.Pizzas.Add(pizza);
            await _dbContext.SaveChangesAsync();
            return pizza;
        }

        public async Task DeleteById(int id)
        {
            var pizza = await _dbContext.Pizzas.FindAsync(id);
            if (pizza is null)
            {
                throw new KeyNotFoundException("Pizza with that id not found");
            }
            _dbContext.Pizzas.Remove(pizza);
            await _dbContext.SaveChangesAsync();
        }

        async Task<List<Pizza>> IPizzasRepository.GetAll()
        {
            return await _dbContext.Pizzas.ToListAsync();
        }
    }
}

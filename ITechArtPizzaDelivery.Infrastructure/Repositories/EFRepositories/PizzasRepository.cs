using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using ITechArtPizzaDelivery.Domain.Errors;
using ITechArtPizzaDelivery.Domain.Interfaces;
using ITechArtPizzaDelivery.Domain.Models;
using ITechArtPizzaDelivery.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ITechArtPizzaDelivery.Infrastructure.Repositories.EFRepositories
{
    public class PizzasRepository : GenericRepository<Pizza>, IPizzasRepository
    {
        public PizzasRepository(PizzaDeliveryContext context) : base(context)
        {
        }

        public async Task<Pizza> GetPizzaWithIngredients(int id)
        {
            var pizza = await _dbContext.Pizzas
                .Include(p => p.Ingredients)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (pizza is null)
            {
                throw new KeyNotFoundException("Pizza not found");
            }

            return pizza;
        }

        public async Task DeleteByIdWithImage(int pizzaId)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync();
            var pizza = await GetById(pizzaId);
            _dbContext.Pizzas.Remove(pizza);
            await _dbContext.SaveChangesAsync();

            if (pizza.Image is not null)
            {
                if (!File.Exists(pizza.Image))
                {
                    throw new ServerErrorException("Can't find image");
                }
                
                File.Delete(pizza.Image);
            }

            await transaction.CommitAsync();
        }

        public async Task<Pizza> AddIngredientsToPizza(int pizzaId, int[] ingredientsId)
        {
            var pizza = await _dbContext.Pizzas
                .Include(p => p.Ingredients)
                .FirstOrDefaultAsync(p => p.Id == pizzaId);
            if (pizza is null)
            {
                throw new KeyNotFoundException("Pizza not found");
            }

            var pizzaIngredientsId = pizza.Ingredients.Select(i => i.Id).ToList();


            var ingredients = await _dbContext.Ingredients
                .Where(i => ingredientsId.Contains(i.Id) && !pizzaIngredientsId.Contains(i.Id))
                .ToListAsync();
            if (ingredients.Count == 0)
            {
                throw new KeyNotFoundException("New ingredients not found");
            }

            pizza.Ingredients.AddRange(ingredients);
            await _dbContext.SaveChangesAsync();
            return pizza;
        }
    }
}

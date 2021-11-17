using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Interfaces;
using ITechArtPizzaDelivery.Domain.Models;
using ITechArtPizzaDelivery.Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITechArtPizzaDelivery.Infrastructure.Repositories.EFRepositories
{
    public class IngredientsRepository : IIngredientsRepository
    {
        private readonly PizzaDeliveryContext _dbContext;

        public IngredientsRepository(PizzaDeliveryContext context)
        {
            _dbContext = context;
        }

        public async Task<Ingredient> GetById(int id)
        {
            return await _dbContext.Ingredients.FindAsync(id);
        }

        public async Task<Ingredient> Post(Ingredient ingredient)
        {
            _dbContext.Ingredients.Add(ingredient);
            await _dbContext.SaveChangesAsync();
            return ingredient;
        }

        public async Task<List<Ingredient>> GetAll()
        {
            return await _dbContext.Ingredients.ToListAsync();
        }

        public async Task DeleteById(int id)
        {
            var ingredient = await _dbContext.Ingredients.FindAsync(id);

            _dbContext.Remove(ingredient);
            await _dbContext.SaveChangesAsync();
        }
    }
}

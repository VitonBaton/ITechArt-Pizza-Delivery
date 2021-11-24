﻿using System;
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
            var pizza = await _dbContext.Pizzas
                .Include(p => p.Ingredients)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (pizza is null)
            {
                throw new KeyNotFoundException("Pizza with that id not found");
            }

            return pizza;
        }

        public async Task<Pizza> Post(Pizza pizza)
        {
            _dbContext.Pizzas.Add(pizza);
            await _dbContext.SaveChangesAsync();
            return pizza;
        }

        public async Task<Pizza> AddIngredientsToPizza(int pizzaId, int[] ingredientsId)
        {
            var pizza = await _dbContext.Pizzas
                .Include(p => p.Ingredients)
                .FirstAsync(p => p.Id == pizzaId);
            if (pizza is null)
            {
                throw new KeyNotFoundException("Pizza with that id not found");
            }

            var pizzaIngredientsId = pizza.Ingredients.Select(i => i.Id).ToList();


            var ingredients = await _dbContext.Ingredients
                .Where(i => ingredientsId.Contains(i.Id) && !pizzaIngredientsId.Contains(i.Id))
                .ToListAsync();
            if (ingredients.Count == 0)
            {
                throw new KeyNotFoundException("New ingredients with that id not found");
            }

            pizza.Ingredients.AddRange(ingredients);
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

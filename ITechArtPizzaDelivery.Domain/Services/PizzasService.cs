using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Interfaces;
using ITechArtPizzaDelivery.Domain.Models;

namespace ITechArtPizzaDelivery.Domain.Services
{
    public class PizzasService : IPizzasService
    {
        private readonly IPizzasRepository _pizzasRepository;
        private readonly IGenericRepository<Ingredient> _ingredientsRepository;

        public PizzasService(IPizzasRepository pizzasRepository, IGenericRepository<Ingredient> ingredientsRepository)
        {
            _pizzasRepository = pizzasRepository;
            _ingredientsRepository = ingredientsRepository;
        }

        public async Task<List<Pizza>> GetAll()
        {
            return await _pizzasRepository.GetAll();
        }

        public async Task<Pizza> GetById(int id)
        {
            return await _pizzasRepository.GetPizzaWithIngredients(id);
        }
        public async Task<Pizza> Post(Pizza pizza)
        {
            return await _pizzasRepository.Insert(pizza);
        }

        public async Task<Pizza> AddIngredientsToPizza(int pizzaId, int[] ingredientsId)
        {
            var pizza = await _pizzasRepository.GetPizzaWithIngredients(pizzaId);
            var pizzaIngredientsId = pizza.Ingredients.Select(i => i.Id).ToList();
            
            var ingredients = await _ingredientsRepository.GetAll();
            var newIngredients = ingredients    
                .Where(i => ingredientsId.Contains(i.Id) && !pizzaIngredientsId.Contains(i.Id))
                .ToList();
            if (newIngredients.Count == 0)
            {
                throw new KeyNotFoundException("New ingredients not found");
            }

            pizza.Ingredients.AddRange(ingredients);
            await _pizzasRepository.Update(pizza);
            return pizza;
        }

        public async Task DeleteById(int id)
        {
            await _pizzasRepository.Delete(id);
        }
    }
}

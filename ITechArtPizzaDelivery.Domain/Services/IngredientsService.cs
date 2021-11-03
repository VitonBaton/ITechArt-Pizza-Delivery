using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Interfaces;
using ITechArtPizzaDelivery.Domain.Models;

namespace ITechArtPizzaDelivery.Domain.Services
{
    public class IngredientsService : IIngredientsService
    {
        private readonly IIngredientsRepository _ingredientsRepository;

        public IngredientsService(IIngredientsRepository ingredientsRepository)
        {
            _ingredientsRepository = ingredientsRepository;
        }

        public async Task<List<Ingredient>> GetAll()
        {
            return await _ingredientsRepository.GetAll();
        }

        public async Task<Ingredient> GetById(long id)
        {
            return await _ingredientsRepository.GetById(id);
        }
        public async Task<Ingredient> Post(Ingredient ingredient)
        {
            return await _ingredientsRepository.Post(ingredient);
        }
    }
}

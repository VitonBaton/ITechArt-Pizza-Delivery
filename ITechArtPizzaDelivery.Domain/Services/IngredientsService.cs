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

        public Task<List<Ingredient>> GetAll()
        {
            return _ingredientsRepository.GetAll();
        }

        public Task<Ingredient> GetById(long id)
        {
            return _ingredientsRepository.GetById(id);
        }
        public Task<Ingredient> Post(Ingredient ingredient)
        {
            return _ingredientsRepository.Post(ingredient);
        }
    }
}

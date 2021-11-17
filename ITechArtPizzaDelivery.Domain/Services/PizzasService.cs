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

        public PizzasService(IPizzasRepository pizzasRepository)
        {
            _pizzasRepository = pizzasRepository;
        }

        public async Task<List<Pizza>> GetAll()
        {
            return await _pizzasRepository.GetAll();
        }

        public async Task<Pizza> GetById(int id)
        {
            return await _pizzasRepository.GetById(id);
        }
        public async Task<Pizza> Post(Pizza pizza, int[] ingredientsId)
        {
            return await _pizzasRepository.Post(pizza, ingredientsId);
        }

        public async Task DeleteById(int id)
        {
            await _pizzasRepository.DeleteById(id);
        }
    }
}

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

        public Task<Pizza> GetById(long id)
        {
            return _pizzasRepository.GetById(id);
        }
        public Task<Pizza> Post(Pizza pizza, long[] ingredientsId)
        {
            return _pizzasRepository.Post(pizza, ingredientsId);
        }
    }
}

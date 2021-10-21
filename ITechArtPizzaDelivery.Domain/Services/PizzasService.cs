using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Interfaces;
using ITechArtPizzaDelivery.Domain.Models;

namespace ITechArtPizzaDelivery.Domain.Services
{
    public class PizzasService
    {
        private readonly IPizzasRepository pizzasRepository;

        public PizzasService(IPizzasRepository _pizzasRepository)
        {
            pizzasRepository = _pizzasRepository ?? throw new ArgumentNullException(nameof(_pizzasRepository), "Repository is null");
        }

        public List<Pizza> GetAll()
        {
            return pizzasRepository.GetAll();
        }

        public Pizza GetById(int id)
        {
            return pizzasRepository.GetById(id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Models;

namespace ITechArtPizzaDelivery.Domain.Interfaces
{
    public interface IPizzasRepository : IGenericRepository<Pizza>
    {
        public Task<Pizza> GetPizzaWithIngredients(int id);
        public Task DeleteByIdWithImage(int pizzaId);
        public Task<Pizza> FindMostPopularForChosenMonth(DateTime time);
    }
}

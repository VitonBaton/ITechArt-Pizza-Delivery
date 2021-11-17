using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Models;

namespace ITechArtPizzaDelivery.Domain.Interfaces
{
    public interface IPizzasRepository
    {
        public Task<List<Pizza>> GetAll();
        public Task<Pizza> GetById(int id);
        public Task<Pizza> Post(Pizza pizza, int[] ingredientsId);
        public Task DeleteById(int id);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Models;

namespace ITechArtPizzaDelivery.Domain.Interfaces
{
    public interface IPizzasService
    {
        Task<List<Pizza>> GetAll();
        Task<Pizza> GetById(int id);
        Task<Pizza> Post(Pizza pizza, int[] ingredientsId);
        public Task DeleteById(int id);
    }
}
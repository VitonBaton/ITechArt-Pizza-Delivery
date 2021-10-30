using System.Collections.Generic;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Models;

namespace ITechArtPizzaDelivery.Domain.Services
{
    public interface IPizzasService
    {
        Task<List<Pizza>> GetAll();
        Task<Pizza> GetById(long id);
        Task<Pizza> Post(Pizza pizza, long[] ingredientsId);
    }
}
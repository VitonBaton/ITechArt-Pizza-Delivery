using System.Collections.Generic;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Models;

namespace ITechArtPizzaDelivery.Domain.Interfaces
{
    public interface IIngredientsService
    {
        Task<List<Ingredient>> GetAll();
        Task<Ingredient> GetById(int id);
        Task<Ingredient> Post(Ingredient ingredient);
        Task DeleteById(int id);
    }
}
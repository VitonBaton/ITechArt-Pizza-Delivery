using System.Collections.Generic;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Models;

namespace ITechArtPizzaDelivery.Domain.Services
{
    public interface IIngredientsService
    {
        Task<List<Ingredient>> GetAll();
        Task<Ingredient> GetById(long id);
        Task<Ingredient> Post(Ingredient ingredient);
    }
}
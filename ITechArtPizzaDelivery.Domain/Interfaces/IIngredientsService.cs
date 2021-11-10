using System.Collections.Generic;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITechArtPizzaDelivery.Domain.Interfaces
{
    public interface IIngredientsService
    {
        Task<List<Ingredient>> GetAll();
        Task<Ingredient> GetById(long id);
        Task<Ingredient> Post(Ingredient ingredient);
        Task<IActionResult> DeleteById(long id);
    }
}
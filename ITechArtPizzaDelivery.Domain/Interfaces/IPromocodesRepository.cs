using System.Collections.Generic;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITechArtPizzaDelivery.Domain.Interfaces
{
    public interface IPromocodesRepository
    {
        Task<Promocode> Post(Promocode promocode);
        Task<List<Promocode>> GetAll();
        Task<Promocode> GetById(long id);
        Task<IActionResult> DeleteById(long id);
    }
}
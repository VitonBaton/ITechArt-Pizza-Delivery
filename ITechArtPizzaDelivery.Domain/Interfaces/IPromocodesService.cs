using System.Collections.Generic;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Models;

namespace ITechArtPizzaDelivery.Domain.Services
{
    public interface IPromocodesService
    {
        Task<List<Promocode>> GetAll();
        Task<Promocode> GetById(long id);
        Task<Promocode> Post(Promocode promocode);
        Task DeleteById(long id);
    }
}
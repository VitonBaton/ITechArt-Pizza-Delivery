using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Models;

namespace ITechArtPizzaDelivery.Domain.Interfaces
{
    public interface IPromocodesRepository : IGenericRepository<Promocode>
    {
        public Task<Promocode> GetPromocodeByName(string promocode);
    }
}
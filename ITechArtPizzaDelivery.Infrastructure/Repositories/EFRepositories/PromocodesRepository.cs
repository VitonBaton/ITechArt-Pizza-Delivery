using System.Collections.Generic;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Interfaces;
using ITechArtPizzaDelivery.Domain.Models;
using ITechArtPizzaDelivery.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ITechArtPizzaDelivery.Infrastructure.Repositories.EFRepositories
{
    public class PromocodesRepository : GenericRepository<Promocode>, IPromocodesRepository
    {
        public PromocodesRepository(PizzaDeliveryContext context) : base(context)
        {
        }

        public async Task<Promocode> GetPromocodeByName(string promocodeName)
        {
            var promocode = await _dbContext.Promocodes
                .FirstOrDefaultAsync(p => p.Name.Equals(promocodeName));

            if (promocode is null)
            {
                throw new KeyNotFoundException("Can't find that promocode");
            }

            return promocode;
        }
    }
}
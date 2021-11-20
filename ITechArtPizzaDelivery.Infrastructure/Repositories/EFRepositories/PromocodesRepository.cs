using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Interfaces;
using ITechArtPizzaDelivery.Domain.Models;
using ITechArtPizzaDelivery.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ITechArtPizzaDelivery.Infrastructure.Repositories.EFRepositories
{
    public class PromocodesRepository : IPromocodesRepository
    {
        private readonly PizzaDeliveryContext _dbContext;

        public PromocodesRepository(PizzaDeliveryContext context)
        {
            _dbContext = context;
        }

        public async Task<Promocode> Post(Promocode promocode)
        {
            await _dbContext.Promocodes.AddAsync(promocode);
            await _dbContext.SaveChangesAsync();
            return promocode;
        }

        public async Task<List<Promocode>> GetAll()
        {
            return await _dbContext.Promocodes.ToListAsync();
        }

        public async Task<Promocode> GetById(int id)
        {
            var promocode = await _dbContext.Promocodes.FindAsync(id);
            if (promocode is null)
            {
                throw new KeyNotFoundException("Promocode with that id not found");
            }

            return promocode;
        }

        public async Task DeleteById(int id)
        {
            var promocode = await _dbContext.Promocodes.FindAsync(id);
            if (promocode is null)
            {
                throw new KeyNotFoundException("Promocode with that id not found");
            }
            _dbContext.Promocodes.Remove(promocode);
            await _dbContext.SaveChangesAsync();
        }
    }
}

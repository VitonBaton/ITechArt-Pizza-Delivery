using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Interfaces;
using ITechArtPizzaDelivery.Domain.Models;
using ITechArtPizzaDelivery.Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<Promocode> GetById(long id)
        {
            return await _dbContext.Promocodes.FindAsync(id);
        }

        public async Task<IActionResult> DeleteById(long id)
        {
            var promocode = await _dbContext.Promocodes
                            .FindAsync(id);
            _dbContext.Promocodes.Remove(promocode);
            await _dbContext.SaveChangesAsync();
            return new OkResult();
        }
    }
}

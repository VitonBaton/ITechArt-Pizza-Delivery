using System.Collections.Generic;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Interfaces;
using ITechArtPizzaDelivery.Domain.Models;

namespace ITechArtPizzaDelivery.Domain.Services
{
    public class PromocodesService : IPromocodesService
    {
        private readonly IPromocodesRepository _promocodesRepository;

        public PromocodesService(IPromocodesRepository promocodesRepository)
        {
            _promocodesRepository = promocodesRepository;
        }

        public async Task DeleteById(int id)
        {
            await _promocodesRepository.DeleteById(id);
        }

        public async Task<List<Promocode>> GetAll()
        {
            return await _promocodesRepository.GetAll();
        }

        public async Task<Promocode> GetById(int id)
        {
            return await _promocodesRepository.GetById(id);
        }

        public async Task<Promocode> Post(Promocode promocode)
        {
            return await _promocodesRepository.Post(promocode);
        }
    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Models;
using ITechArtPizzaDelivery.Domain.Services;

namespace ITechArtPizzaDelivery.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromocodesController : ControllerBase
    {
        private readonly IPromocodesService _promocodesService;

        public PromocodesController(IPromocodesService service)
        {
            _promocodesService = service;
        }

        [HttpGet]
        public async Task<List<Promocode>> GetAll()
        {
            return await _promocodesService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<Promocode> GetById(long id)
        {
            return await _promocodesService.GetById(id);
        }

        [HttpPost]
        public async Task<Promocode> Post(Promocode promocode)
        {

            return await _promocodesService.Post(promocode);
        }

        [HttpDelete("{id}")]
        public async Task DeleteById(long id)
        {
            await _promocodesService.DeleteById(id);
        }
    }
}

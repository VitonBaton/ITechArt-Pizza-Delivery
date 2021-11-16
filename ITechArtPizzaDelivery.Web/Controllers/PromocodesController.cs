using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ITechArtPizzaDelivery.Domain.Interfaces;
using ITechArtPizzaDelivery.Domain.Models;
using ITechArtPizzaDelivery.Domain.Services;
using ITechArtPizzaDelivery.Web.Models;

namespace ITechArtPizzaDelivery.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromocodesController : ControllerBase
    {
        private readonly IPromocodesService _promocodesService;
        private readonly IMapper _mapper;
        public PromocodesController(IPromocodesService service, IMapper mapper)
        {
            _promocodesService = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<GetPromocodeModel>> GetAll()
        {
            var promocodes = await _promocodesService.GetAll();
            return _mapper.Map<List<GetPromocodeModel>>(promocodes);
        }

        [HttpGet("{id}")]
        public async Task<GetPromocodeModel> GetById(long id)
        {
            var promocode = await _promocodesService.GetById(id);
            return _mapper.Map<GetPromocodeModel>(promocode);
        }

        [HttpPost]
        public async Task<GetPromocodeModel> Post(PostPromocodeModel model)
        {
            var promocode = _mapper.Map<PostPromocodeModel, Promocode>(model);
            var newPromocode = await _promocodesService.Post(promocode);
            return _mapper.Map<GetPromocodeModel>(newPromocode);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(long id)
        {
            await _promocodesService.DeleteById(id);
            return Ok();
        }
    }
}

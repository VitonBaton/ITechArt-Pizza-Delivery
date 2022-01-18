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
using Microsoft.AspNetCore.Authorization;

namespace ITechArtPizzaDelivery.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
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
        public async Task<ActionResult<List<GetPromocodeModel>>> GetAll()
        {
            var promocodes = await _promocodesService.GetAll();
            return Ok(_mapper.Map<List<GetPromocodeModel>>(promocodes));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetPromocodeModel>> GetById(int id)
        {
            try
            {
                var promocode = await _promocodesService.GetById(id);
                return Ok(_mapper.Map<GetPromocodeModel>(promocode));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<GetPromocodeModel>> Post(PostPromocodeModel model)
        {
            try
            {
                var promocode = _mapper.Map<PostPromocodeModel, Promocode>(model);
                var newPromocode = await _promocodesService.Post(promocode);
                return Ok(_mapper.Map<GetPromocodeModel>(newPromocode));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            try
            {
                await _promocodesService.DeleteById(id);
                return Ok();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}

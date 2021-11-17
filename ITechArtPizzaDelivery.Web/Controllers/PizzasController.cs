using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ITechArtPizzaDelivery.Domain.Interfaces;
using ITechArtPizzaDelivery.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ITechArtPizzaDelivery.Domain.Services;
using ITechArtPizzaDelivery.Infrastructure.Repositories;
using ITechArtPizzaDelivery.Web.Models;

namespace ITechArtPizzaDelivery.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        private readonly IPizzasService _pizzasService;
        private readonly IMapper _mapper;

        public PizzasController(IPizzasService service, IMapper mapper)
        {
            _pizzasService = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetPizzaModel>>> GetAll()
        {
            var pizzas = await _pizzasService.GetAll();
            var pizzasView = _mapper.Map<List<Pizza>, List<GetPizzaModel>>(pizzas);
            return Ok(pizzasView);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetPizzaWithIngredientsModel>> GetById(int id)
        {
            var pizza = await _pizzasService.GetById(id);
            var pizzaView = _mapper.Map<Pizza, GetPizzaWithIngredientsModel>(pizza);
            return Ok(pizzaView);
        }

        [HttpPost]
        public async Task<ActionResult<GetPizzaWithIngredientsModel>> Post(PostPizzaModel model)
        {
            var pizza = _mapper.Map<PostPizzaModel, Pizza>(model);
            var newPizza = await _pizzasService.Post(pizza, model.IngredientsId);
            return _mapper.Map<GetPizzaWithIngredientsModel>(newPizza);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            try
            {
                await _pizzasService.DeleteById(id);
                return Ok();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

    }
}

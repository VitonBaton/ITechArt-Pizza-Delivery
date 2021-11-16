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
        public async Task<List<GetAllPizzasModel>> GetAll()
        {
            var pizzas = await _pizzasService.GetAll();
            var pizzasView = _mapper.Map<List<Pizza>, List<GetAllPizzasModel>>(pizzas);
            return pizzasView;
        }

        [HttpGet("{id}")]
        public async Task<GetPizzaModel> GetById(long id)
        {
            var pizza = await _pizzasService.GetById(id);
            var pizzaView = _mapper.Map<Pizza, GetPizzaModel>(pizza);
            return pizzaView;
        }

        [HttpPost]
        public async Task<GetPizzaModel> Post(PostPizzaModel model)
        {
            var pizza = _mapper.Map<PostPizzaModel, Pizza>(model);
            var newPizza = await _pizzasService.Post(pizza, model.IngredientsId);
            return _mapper.Map<GetPizzaModel>(newPizza);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(long id)
        {
            await _pizzasService.DeleteById(id);
            return Ok();
        }

    }
}

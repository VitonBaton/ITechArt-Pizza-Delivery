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

        public PizzasController(IPizzasService service)
        {
            _pizzasService = service;
        }

        [HttpGet]
        public async Task<List<Pizza>> GetAll()
        {
            return await _pizzasService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<Pizza> GetById(long id)
        {
            return await _pizzasService.GetById(id);
        }

        [HttpPost]
        public async Task<Pizza> Post(PostPizzaModel model)
        {
            var config = new MapperConfiguration(cfg => 
                cfg.CreateMap<PostPizzaModel, Pizza>());
            var mapper = config.CreateMapper();
            var pizza = mapper.Map<PostPizzaModel, Pizza>(model);
            return await _pizzasService.Post(pizza, model.IngredientsId);
        }

        [HttpDelete("{id}")]
        public async Task DeleteById(long id)
        {
            await _pizzasService.DeleteById(id);
        }

    }
}

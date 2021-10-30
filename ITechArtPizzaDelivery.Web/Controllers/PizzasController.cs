using System.Collections.Generic;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ITechArtPizzaDelivery.Domain.Services;
using ITechArtPizzaDelivery.Infrastructure.Repositories;

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
        public Task<List<Pizza>> GetAll()
        {
            return _pizzasService.GetAll();
        }

        [HttpGet("{id}")]
        public Task<Pizza> GetById(long id)
        {
            return _pizzasService.GetById(id);
        }

        [HttpPost]
        public Task<Pizza> Post(Pizza pizza, [FromQuery] long[] ingredientsId)
        {
            
            return _pizzasService.Post(pizza, ingredientsId);
        }
    }
}

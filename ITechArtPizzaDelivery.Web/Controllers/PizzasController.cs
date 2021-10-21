using System.Collections.Generic;
using ITechArtPizzaDelivery.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ITechArtPizzaDelivery.Domain.Services;
using ITechArtPizzaDelivery.Infrastructure.Repositories.Fakes;

namespace ITechArtPizzaDelivery.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        private readonly PizzasService _pizzasService = new(new PizzasFakeRepository());

        [HttpGet]
        public List<Pizza> GetAll()
        {
            return _pizzasService.GetAll();
        }

        [HttpGet("{id}")]
        public Pizza GetById(int id)
        {
            return _pizzasService.GetById(id);
        }
    }
}

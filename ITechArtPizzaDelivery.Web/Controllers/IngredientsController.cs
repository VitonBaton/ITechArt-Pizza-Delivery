using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Models;
using ITechArtPizzaDelivery.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace ITechArtPizzaDelivery.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientsService _ingredientsService;

        public IngredientsController(IIngredientsService service)
        {
            _ingredientsService = service;
        }

        [HttpGet]
        public Task<List<Ingredient>> GetAll()
        {
            return _ingredientsService.GetAll();
        }

        [HttpGet("{id}")]
        public Task<Ingredient> GetById(int id)
        {
            return _ingredientsService.GetById(id);
        }

        [HttpPost]
        public Task<Ingredient> Post(Ingredient ingredient)
        {
            return _ingredientsService.Post(ingredient);
        }
    }
}

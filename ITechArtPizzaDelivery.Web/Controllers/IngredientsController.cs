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
        public async Task<List<Ingredient>> GetAll()
        {
            return await _ingredientsService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<Ingredient> GetById(int id)
        {
            return await _ingredientsService.GetById(id);
        }

        [HttpPost]
        public async Task<Ingredient> Post(Ingredient ingredient)
        {
            return await _ingredientsService.Post(ingredient);
        }
    }
}

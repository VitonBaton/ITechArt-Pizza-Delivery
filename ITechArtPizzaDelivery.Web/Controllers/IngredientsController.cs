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
using Microsoft.AspNetCore.Mvc;

namespace ITechArtPizzaDelivery.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientsService _ingredientsService;
        private readonly IMapper _mapper;

        public IngredientsController(IIngredientsService service, IMapper mapper)
        {
            _ingredientsService = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetIngredientModel>>> GetAll()
        {
            var ingredients = await _ingredientsService.GetAll();
            var viewIngredients = _mapper.Map<List<GetIngredientModel>>(ingredients);
            return Ok(viewIngredients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetIngredientModel>> GetById(int id)
        {
            try
            {
                var ingredient = await _ingredientsService.GetById(id);
                return Ok(_mapper.Map<GetIngredientModel>(ingredient));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<GetIngredientModel>> Post(PostIngredientModel model)
        {
            var ingredient = _mapper.Map<Ingredient>(model);
            var newIngredient = await _ingredientsService.Post(ingredient);
            return Ok(_mapper.Map<GetIngredientModel>(newIngredient));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            try
            {
                await _ingredientsService.DeleteById(id);
                return Ok();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}

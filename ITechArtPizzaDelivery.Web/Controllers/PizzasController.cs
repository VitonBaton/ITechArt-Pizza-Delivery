using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ITechArtPizzaDelivery.Domain.Interfaces;
using ITechArtPizzaDelivery.Domain.Models;
using ITechArtPizzaDelivery.Domain.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ITechArtPizzaDelivery.Domain.Services;
using ITechArtPizzaDelivery.Infrastructure.Repositories;
using ITechArtPizzaDelivery.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;

namespace ITechArtPizzaDelivery.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles="Admin,User")]
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
        public async Task<ActionResult<List<GetPizzaModel>>> GetAll([FromQuery] PagingParameters parameters)
        {
            var pizzas = await _pizzasService.GetAll(parameters);
            
            var metadata = new
            {
                pizzas.TotalCount,
                pizzas.PageSize,
                pizzas.CurrentPage,
                pizzas.TotalPages,
                pizzas.HasNext,
                pizzas.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            
            var pizzasView = _mapper.Map<List<Pizza>, List<GetPizzaModel>>(pizzas);
            return Ok(pizzasView);
        }
        
        [HttpGet("popular")]
        public async Task<ActionResult<GetPizzaModel>> FindMostPopularPizza([FromQuery] DateTime time)
        {
            var pizza = await _pizzasService.FindMostPopularForChosenMonth(time);
            var pizzaView = _mapper.Map<Pizza, GetPizzaModel>(pizza);
            return Ok(pizzaView);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetPizzaWithIngredientsModel>> GetById(int id)
        {
            var pizza = await _pizzasService.GetById(id);
            var pizzaView = _mapper.Map<Pizza, GetPizzaWithIngredientsModel>(pizza);
            return Ok(pizzaView);
        }

        [Authorize(Roles="Admin")]
        [HttpPost]
        public async Task<ActionResult<GetPizzaModel>> Post(PostPizzaModel model)
        {
            var pizza = _mapper.Map<PostPizzaModel, Pizza>(model);
            var newPizza = await _pizzasService.Post(pizza);
            return Ok(_mapper.Map<GetPizzaModel>(newPizza));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{pizzaId}/ingredients")]
        public async Task<ActionResult<GetPizzaWithIngredientsModel>> PostIngredientsToPizza(int pizzaId,
            [FromBody] PostPizzaIngredientsModel model)
        {
            var pizza = await _pizzasService.AddIngredientsToPizza(pizzaId, model.IngredientsId);
            return Ok(_mapper.Map<GetPizzaWithIngredientsModel>(pizza));
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPost("{pizzaId}/image")]
        public async Task<IActionResult> PostImageToPizza(int pizzaId, IFormFile image)
        {
            
            await _pizzasService.AddImageToPizza(pizzaId, image);
            return Ok("Image successfully uploaded");
        }
        
        [HttpGet("{pizzaId}/image")]
        public async Task<IActionResult> DownloadImageOfPizza(int pizzaId)
        {
            var image = await _pizzasService.DownloadImage(pizzaId);
            return File(image, "application/image");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            await _pizzasService.DeleteById(id);
            return Ok();
        }

    }
}

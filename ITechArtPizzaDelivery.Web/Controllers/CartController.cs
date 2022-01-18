using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
    [Authorize(Roles = "User")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;
        private int UserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        public CartController(ICartService service, IMapper mapper)
        {
            _cartService = service;
            _mapper = mapper;
        }

        
        [HttpGet]
        public async Task<ActionResult<List<GetCartModel>>> GetAll()
        {
            try
            {
                return _mapper.Map<List<GetCartModel>>(await _cartService.GetPizzasFromCart(UserId));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<GetCartModel>>> Post(PostPizzaToCartModel model)
        {
            try
            {
                var cart = await _cartService.AddPizzaToCart(model.PizzaId, UserId, model.PizzasCount);
                return Ok(_mapper.Map<List<GetCartModel>>(cart));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch("pizzas/{pizzaId}")]
        public async Task<ActionResult> PutPizzaCount(PostPizzaToCartModel model)
        {
            try
            {
                await _cartService.ChangeAmountOfPizza(model.PizzaId, UserId, model.PizzasCount);
                return Ok();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete("pizzas/{pizzaId}")]
        public async Task<ActionResult> DeletePizzaById(int pizzaId)
        {
            try
            {
                await _cartService.DeleteByPizzaId(UserId, pizzaId);
                return Ok();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}

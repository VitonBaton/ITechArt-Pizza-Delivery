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
            return _mapper.Map<List<GetCartModel>>(await _cartService.GetPizzasFromCart(UserId));
        }

        [HttpPost]
        public async Task<ActionResult<List<GetCartModel>>> Post(PostPizzaToCartModel model)
        {
            var cart = await _cartService.AddPizzaToCart(model.PizzaId, UserId, model.PizzasCount);
            return Ok(_mapper.Map<List<GetCartModel>>(cart));
        }

        [HttpPatch("pizzas/{pizzaId}")]
        public async Task<ActionResult> PutPizzaCount(PostPizzaToCartModel model)
        {
            await _cartService.ChangeAmountOfPizza(model.PizzaId, UserId, model.PizzasCount);
            return Ok();
        }

        [HttpDelete("pizzas/{pizzaId}")]
        public async Task<ActionResult> DeletePizzaById(int pizzaId)
        {
            await _cartService.DeleteByPizzaId(UserId, pizzaId);
            return Ok();
        }
    }
}

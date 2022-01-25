using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using ITechArtPizzaDelivery.Domain.Interfaces;
using ITechArtPizzaDelivery.Domain.Models;
using ITechArtPizzaDelivery.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace ITechArtPizzaDelivery.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;
        private readonly IMapper _mapper;
        private int UserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        public OrdersController(IOrdersService service, IMapper mapper)
        {
            _ordersService = service;
            _mapper = mapper;
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<ActionResult<GetOrderWithPizzasModel>> PostOrder(PostOrderModel model)
        {
            var order = await _ordersService.CreateNewOrder(UserId, _mapper.Map<Order>(model));
            return Ok(_mapper.Map<GetOrderWithPizzasModel>(order));
        }

        [Authorize(Roles = "User")]
        [HttpPatch("{orderId}/cancel")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            await _ordersService.CancelOrder(UserId, orderId);
            return Ok("Order successfully canceled");
        }
        
        [Authorize(Roles = "User")]
        [HttpPatch("{orderId}/pay")]
        public async Task<IActionResult> PayOrder(int orderId)
        {
            await _ordersService.PayOrder(UserId, orderId);
            return Ok("Order successfully payed");
        }
        
        [Authorize(Roles = "User")]
        [HttpPatch("{orderId}/deliver")]
        public async Task<IActionResult> DeliverOrder(int orderId)
        {
            await _ordersService.DeliverOrder(UserId, orderId);
            return Ok("Order successfully delivered");
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<ActionResult<List<GetOrderWithPizzasModel>>> GetOrders()
        {
            var orders = await _ordersService.GetCustomerOrders(UserId);
            return Ok(_mapper.Map<List<GetOrderWithPizzasModel>>(orders));
        }

        [Authorize(Roles = "User")]
        [HttpPatch("{orderId}/promocode")]
        public async Task<ActionResult<AddingPromocodeResult>> PatchPromocodeToOrder(int orderId,
            [FromBody] AddPromocodeToOrderModel promocode)
        {
            var result = await _ordersService.AddPromocodeToOrder(UserId, orderId, promocode.PromocodeName);
            return Ok(new AddingPromocodeResult(){NewPrice = result});
        }

        [Authorize(Roles = "User")]
        [HttpDelete("{orderId}/promocode")]
        public async Task<ActionResult<AddingPromocodeResult>> RemovePromocodeFromOrder(int orderId)
        {
            var result = await _ordersService.RemovePromocodeFromOrder(UserId, orderId);
            return Ok(new AddingPromocodeResult(){NewPrice = result});
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<ActionResult<List<GetUserWithOrdersModel>>> GetAllUsersWithOrder()
        {
            var users = await _ordersService.GetAllUsersAndOrders();
            return Ok(_mapper.Map<List<GetUserWithOrdersModel>>(users));
        }
    }
}

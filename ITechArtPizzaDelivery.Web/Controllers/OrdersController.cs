using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ITechArtPizzaDelivery.Domain.Interfaces;
using ITechArtPizzaDelivery.Domain.Models;
using ITechArtPizzaDelivery.Web.Models;

namespace ITechArtPizzaDelivery.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersRepository _ordersService;
        private readonly IMapper _mapper;

        public OrdersController(IOrdersRepository service, IMapper mapper)
        {
            _ordersService = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<GetPlacedOrderModel>> PostOrder(PostOrderModel model)
        {
            try
            {
                var order = await _ordersService.CreateNewOrder(1, model.Address, model.DeliveryId, model.PaymentId,
                    model.Comment);
                return Ok(_mapper.Map<GetPlacedOrderModel>(order));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPatch("{orderId}/cancel")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            try
            {
                await _ordersService.CancelOrder(orderId);
                return Ok("Order successfully canceled");
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<GetOrderWithPizzasModel>>> GetOrders()
        {
            try
            {
                var orders = await _ordersService.GetCustomerOrders(1);
                return Ok(_mapper.Map<List<GetOrderWithPizzasModel>>(orders));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPatch("{orderId}/promocode")]
        public async Task<IActionResult> PatchPromocodeToOrder(int orderId, [FromBody]AddPromocodeToOrderModel promocode)
        {
            try
            {
                await _ordersService.AddPromocodeToOrder(orderId, promocode.PromocodeName);
                return Ok("Promocode successfully added");
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

        [HttpGet("all")]
        public async Task<ActionResult<List<GetUserWithOrdersModel>>> GetAllUsersWithOrder()
        {
            var users = await _ordersService.GetAllUsersAndOrders();
            return Ok(_mapper.Map<List<GetUserWithOrdersModel>>(users));
        }
    }
}

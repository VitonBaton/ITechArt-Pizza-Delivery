﻿using Microsoft.AspNetCore.Http;
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
        private readonly IOrdersRepository _ordersService;
        private readonly IMapper _mapper;
        private int UserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        public OrdersController(IOrdersRepository service, IMapper mapper)
        {
            _ordersService = service;
            _mapper = mapper;
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<ActionResult<GetPlacedOrderModel>> PostOrder(PostOrderModel model)
        {
            try
            {
                var order = await _ordersService.CreateNewOrder(UserId, model.Address, model.DeliveryId, model.PaymentId,
                    model.Comment);
                return Ok(_mapper.Map<GetPlacedOrderModel>(order));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [Authorize(Roles = "User")]
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

        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<ActionResult<List<GetOrderWithPizzasModel>>> GetOrders()
        {
            try
            {
                var orders = await _ordersService.GetCustomerOrders(UserId);
                return Ok(_mapper.Map<List<GetOrderWithPizzasModel>>(orders));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [Authorize(Roles = "User")]
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

        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<ActionResult<List<GetUserWithOrdersModel>>> GetAllUsersWithOrder()
        {
            var users = await _ordersService.GetAllUsersAndOrders();
            return Ok(_mapper.Map<List<GetUserWithOrdersModel>>(users));
        }
    }
}

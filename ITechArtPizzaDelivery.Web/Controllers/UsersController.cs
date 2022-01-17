﻿using Microsoft.AspNetCore.Http;
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
using ITechArtPizzaDelivery.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;

namespace ITechArtPizzaDelivery.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;
        private int UserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        public UsersController(IUsersService service, IMapper mapper)
        {
            _usersService = service;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegistrationModel model)
        {
            try
            {
                await _usersService.Register(_mapper.Map<User>(model), model.Password);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenModel>> Login(LoginModel model)
        {
            try
            {
                var token = await _usersService.Login(model.Login, model.Password, "SUPERMEGAsecretString");
                return Ok(new TokenModel{Token = token});
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> DeleteAccount()
        {
            await _usersService.DeleteAccount(UserId);
            return Ok();
        }
        
    }
}
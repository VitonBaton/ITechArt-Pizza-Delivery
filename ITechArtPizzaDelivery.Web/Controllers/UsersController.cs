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
using ITechArtPizzaDelivery.Domain.Pagination;
using ITechArtPizzaDelivery.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;

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
            await _usersService.Register(_mapper.Map<User>(model), model.Password);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenModel>> Login(LoginModel model)
        {
            var token = await _usersService.Login(model.Login, model.Password, "SUPERMEGAsecretString");
            return Ok(new TokenModel { Token = token });
        }

        [HttpDelete]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> DeleteAccount()
        {
            await _usersService.DeleteAccount(UserId);
            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<User>>> GetAll([FromQuery] PagingParameters parameters)
        {
            var users = await _usersService.GetAll(parameters);
            
            var metadata = new
            {
                users.TotalCount,
                users.PageSize,
                users.CurrentPage,
                users.TotalPages,
                users.HasNext,
                users.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(users);
        }
        
    }
}

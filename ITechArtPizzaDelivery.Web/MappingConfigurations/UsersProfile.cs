using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using ITechArtPizzaDelivery.Domain.Models;
using ITechArtPizzaDelivery.Web.Models;

namespace ITechArtPizzaDelivery.Web.MappingConfigurations
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<RegistrationModel, User>();
        }
    }
}
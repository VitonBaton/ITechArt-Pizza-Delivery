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
            /*CreateMap<JwtSecurityToken, TokenModel>()
                .ForMember(t => t.Token,
                    opt => opt.MapFrom(t => t.S .EncodedPayload));*/
        }
    }
}
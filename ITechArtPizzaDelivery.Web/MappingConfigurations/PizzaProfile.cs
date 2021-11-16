using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ITechArtPizzaDelivery.Domain.Models;
using ITechArtPizzaDelivery.Web.Models;

namespace ITechArtPizzaDelivery.Web.MappingConfigurations
{
    public class PizzaProfile : Profile
    {
        public PizzaProfile()
        {
            CreateMap<Pizza, GetAllPizzasModel>();
            CreateMap<Pizza, GetPizzaModel>();
            CreateMap<PostPizzaModel, Pizza>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ITechArtPizzaDelivery.Domain.Models;
using ITechArtPizzaDelivery.Web.Models;

namespace ITechArtPizzaDelivery.Web.MappingConfigurations
{
    public class IngredientProfile : Profile
    {
        public IngredientProfile()
        {
            CreateMap<PostIngredientModel, Ingredient>();
            CreateMap<Ingredient, GetIngredientModel>();
        }
    }
}

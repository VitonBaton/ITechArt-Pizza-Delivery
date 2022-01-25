using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ITechArtPizzaDelivery.Domain.Models;
using ITechArtPizzaDelivery.Web.Models;

namespace ITechArtPizzaDelivery.Web.MappingConfigurations
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, GetOrderModel>();
            CreateMap<PostOrderModel, Order>();
            CreateMap<Order, GetOrderWithPizzasModel>()
                .ForMember(o => o.Pizzas, opt => opt.MapFrom(o => o.Cart.CartPizzas));
            CreateMap<Order, GetPlacedOrderModel>();
            CreateMap<User, GetUserWithOrdersModel>()
                .ForMember(u => u.FullName, opt => opt.MapFrom(u => u.FirstName + " " + u.LastName));
        }
    }
}

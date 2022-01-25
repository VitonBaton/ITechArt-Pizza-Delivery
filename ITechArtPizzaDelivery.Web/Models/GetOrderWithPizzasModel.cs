using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Models;

namespace ITechArtPizzaDelivery.Web.Models
{
    public class GetOrderWithPizzasModel
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string DeliveryName { get; set; }
        public string PaymentName { get; set; }
        public string Comment { get; set; }
        public string? PromocodeName { get; set; }
        public DateTime CreateAt { get; set; }
        public decimal Price { get; set; }
        public Order.StatusType Status { get; set; }
        public List<GetCartModel> Pizzas { get; set; }
    }
}

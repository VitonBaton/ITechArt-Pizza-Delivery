using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITechArtPizzaDelivery.Web.Models
{
    public class GetPlacedOrderModel
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string DeliveryName { get; set; }
        public string PaymentName { get; set; }
        public string Comment { get; set; }
        public DateTime CreateAt { get; set; }
        public decimal Price { get; set; }
        public List<GetPizzaModel> CartPizzas { get; set; }
    }
}

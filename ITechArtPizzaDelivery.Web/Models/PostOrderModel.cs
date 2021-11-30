using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITechArtPizzaDelivery.Web.Models
{
    public class PostOrderModel
    {
        public string Address { get; set; }
        public int DeliveryId { get; set; }
        public int PaymentId { get; set; }
        public string Comment { get; set; }
    }
}

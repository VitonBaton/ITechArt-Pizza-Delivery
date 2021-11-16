using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITechArtPizzaDelivery.Domain.Models
{
    public class CartPizza
    {
        public long CartId { get; set; }
        public Cart Cart { get; set; }
        public long PizzaId { get; set; }
        public Pizza Pizza { get; set; }
        public int Count { get; set; }
    }
}

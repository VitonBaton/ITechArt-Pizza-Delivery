using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITechArtPizzaDelivery.Domain.Models
{
    public class CartPizza
    {
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public int PizzaId { get; set; }
        public Pizza Pizza { get; set; }
        public int Count { get; set; }
    }
}

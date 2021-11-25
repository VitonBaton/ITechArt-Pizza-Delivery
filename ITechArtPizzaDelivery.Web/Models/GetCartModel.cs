using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Models;

namespace ITechArtPizzaDelivery.Web.Models
{
    public class GetCartModel
    {
        public int PizzaId { get; set; }
        public string PizzaName { get; set; }
        public int Count { get; set; }
    }
}

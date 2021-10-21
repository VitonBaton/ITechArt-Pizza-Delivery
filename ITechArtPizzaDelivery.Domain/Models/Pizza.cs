using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITechArtPizzaDelivery.Domain.Models
{
    public class Pizza
    {
        public long Id { get; set; }
        public string Name { get; set; }
        //public string Image { get; set; }
        public decimal Price { get; set; }
        public List<string> Ingredients { get; set; }
        //public List<Ingredients> Ingredients { get; set; }
    }
}

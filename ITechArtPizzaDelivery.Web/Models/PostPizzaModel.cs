using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITechArtPizzaDelivery.Web.Models
{
    public class PostPizzaModel
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public long[] IngredientsId { get; set; }
    }
}

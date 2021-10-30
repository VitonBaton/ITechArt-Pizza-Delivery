using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITechArtPizzaDelivery.Domain.Models
{
    public class Ingredient
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Pizza> Pizzas { get; set; }
    }
}

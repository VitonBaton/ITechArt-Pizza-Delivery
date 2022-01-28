using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ITechArtPizzaDelivery.Domain.Models
{
    public class Pizza
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Image { get; set; }
        [Required]
        [Column(TypeName = "decimal(5, 2)")]
        public decimal Price { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Cart> Carts { get; set; }
        public List<CartPizza> CartPizzas { get; set; }
    }
}

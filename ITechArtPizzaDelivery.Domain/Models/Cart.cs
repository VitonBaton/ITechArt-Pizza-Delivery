using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITechArtPizzaDelivery.Domain.Models
{
    public class Cart
    {
        public long Id { get; set; }
        [Required]
        public int PizzaCount { get; set; }
        [Required]
        public Pizza Pizza { get; set; }
        [Required]
        public User Customer { get; set; }
        public Order Order { get; set; }
    }
}

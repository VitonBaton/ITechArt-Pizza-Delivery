#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITechArtPizzaDelivery.Domain.Models
{
    public class Cart
    {
        public int Id { get; set; }
        //public int CustomerId { get; set; }
        //[Required]
        //[ForeignKey("CustomerId")]
        //public User Customer { get; set; }
        public int? OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order? Order { get; set; }
        public List<Pizza>? Pizzas { get; set; }
        public List<CartPizza>? CartPizzas { get; set; }
    }
}

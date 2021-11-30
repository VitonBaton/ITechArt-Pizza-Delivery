using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Models;

namespace ITechArtPizzaDelivery.Infrastructure.DataSeeds
{
    public static class DeliverySeeds
    {
        private static readonly List<Delivery> Deliveries = new()
        {
            new Delivery()
            {
                Id = 1,
                Name = "Pick up",
                Description = "Pick up pizza from the nearest pizzeria",
                Price = 0
            },
            new Delivery()
            {
                Id = 2,
                Name = "Courier",
                Description = "Fast delivery",
                Price = 10
            }
        };

        public static List<Delivery> GetDeliveries()
        {
            return Deliveries;
        }
    }
}

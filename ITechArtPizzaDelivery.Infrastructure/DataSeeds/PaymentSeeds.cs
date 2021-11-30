using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Models;

namespace ITechArtPizzaDelivery.Infrastructure.DataSeeds
{
    public static class PaymentSeeds
    {
        private static readonly List<Payment> Payments = new()
        {
            new Payment()
            {
                Id = 1,
                Name = "Card online",
                Description = "Pay order online"
            },
            new Payment()
            {
                Id = 2,
                Name = "Card",
                Description = "Pay order to the courier by card"
            },
            new Payment()
            {
                Id = 3,
                Name = "Cash",
                Description = "Pay order to the courier in cash"
            }
        };

        public static List<Payment> GetPayments()
        {
            return Payments;
        }
    }
}

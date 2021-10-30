using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITechArtPizzaDelivery.Domain.Models
{
    public class Payment
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Order Order { get; set; }
    }
}

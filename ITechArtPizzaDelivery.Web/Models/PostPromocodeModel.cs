using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITechArtPizzaDelivery.Web.Models
{
    public class PostPromocodeModel
    {
        public string Name { get; set; }
        public decimal Discount { get; set; }
    }
}

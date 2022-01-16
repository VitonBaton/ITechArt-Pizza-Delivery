#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ITechArtPizzaDelivery.Domain.Models
{
    public class User : IdentityUser<int>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? CartId { get; set; }
        public Cart? Cart { get; set; }
        public List<Order>? Orders { get;set; }
    }
}

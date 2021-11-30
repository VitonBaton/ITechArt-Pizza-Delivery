#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITechArtPizzaDelivery.Domain.Models
{
    public enum Role
    {
        Admin,
        User
    }

    public class User
    {
        public int Id { get; set; }
        [Required]
        public Role Role { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? CartId { get; set; }
        public Cart? Cart { get; set; }
        public List<Order>? Orders { get;set; }
    }
}

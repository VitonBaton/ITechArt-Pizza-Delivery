﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITechArtPizzaDelivery.Domain.Models
{
    public class Promocode
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "decimal(5, 2)")]
        public decimal Discount { get; set; }
        public List<Order> Orders { get; set; }
    }
}
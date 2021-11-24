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

    public class Order
    {
        public enum StatusType
        {
            Delivered,
            New,
            Payed,
            Canceled,
            Done
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public User Customer { get; set; }
        [Required]
        public string Address { get; set; }
        public int DeliveryId { get; set; }
        [Required]
        public Delivery Delivery { get; set; }
        public int PaymentId { get; set; }
        [Required]
        public Payment? Payment { get; set; }
        [Required]
        public string Comment { get; set; }
        public int? PromocodeId { get; set; }
        public Promocode? Promocode { get; set; }
        public DateTime CreateAt { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        [Required]
        public decimal Price { get; set; }
        public StatusType Status { get; set; }
    }
}

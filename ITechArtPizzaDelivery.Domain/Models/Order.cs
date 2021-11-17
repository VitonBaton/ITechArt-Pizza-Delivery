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
        [ForeignKey("CustomerId")]
        public User Customer { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [ForeignKey("DeliveryId")]
        public Delivery Delivery { get; set; }
        [Required]
        [ForeignKey("PaymentId")]
        public Payment Payment { get; set; }
        [Required]
        public string Comment { get; set; }
        [ForeignKey("PromocodeId")]
        public Promocode Promocode { get; set; }
        public DateTime CreateAt { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        [Required]
        public decimal Price { get; set; }
        public StatusType Status { get; set; }
    }
}

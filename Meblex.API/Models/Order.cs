using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meblex.API.Models
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        public int TransactionId { get; set; }

        [Required]
        [StringLength(32)]
        public string Delivery { get; set; }

        [Required]
        public bool Reservation { get; set; }

        [Required]
        [StringLength(32)]
        public string Street { get; set; }

        [Required]
        [StringLength(32)]
        public string Address { get; set; }

        [Required]
        [StringLength(32)]
        public string State { get; set; }

        [Required]
        [StringLength(32)]
        public string City { get; set; }

        [Required]
        public int PostCode { get; set; }

        public virtual List<OrderLine> OrderLines { get; set; }

        [Required]
        public virtual Client Client { get; set; }

        [Required]
        public int ClientId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meblex.API.Models
{
    public class Client
    {
        public Client()
        {
            Orders = new List<Order>();
            CustomSizeForms = new List<CustomSizeForm>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int  ClientId { get; set; }

        [Required]
        [StringLength(32)]
        public string Name { get; set; }


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

        public string NIP { get; set; }

        [Required]
        public virtual User User { get; set; }
        [Required]
        public int UserId { get; set; }

        public virtual List<CustomSizeForm> CustomSizeForms { get; set; }

        public virtual List<Order> Orders { get; set; }
    }
}
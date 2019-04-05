using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meblex.API.Models
{
    public class Client
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int  ClientId { get; set; }

        [Required]
        [StringLength(32)]
        public string Name { get; set; }

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

        [Required]
        public User User { get; set; }
        [Required]
        public int UserId { get; set; }

        public List<CustomSizeForm> CustomSizeForms { get; set; }

        public List<Order> Orders { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meblex.API.Models
{
    public class OrderLine
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderLineId { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        [StringLength(32)]
        public string Size { get; set; }


        public virtual PieceOfFurniture PieceOfFurniture { get; set; }


        public int? PieceOfFurnitureId { get; set; }


        public virtual Part Part { get; set; }


        public int? PartId { get; set; }

        [Required]
        public virtual Order Order { get; set; }

        [Required]
        public int OrderId { get; set; }
    }
}

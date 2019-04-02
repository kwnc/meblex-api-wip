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


        public PieceOfFurniture PieceOfFurniture { get; set; }


        public int? PieceOfFurnitureId
        {
            get => PieceOfFurnitureId;
            set
            {
                if (PartId != null )
                {
                    throw new Exception("Relation with Part exist");
                }
                else
                {
                    PieceOfFurnitureId = value;
                }
            }
        }


        public Part Part { get; set; }


        public int? PartId
        {
            get => PartId;
            set
            {
                if (PieceOfFurnitureId != null)
                {
                    throw new Exception("Relation with Part exist");
                }
                else
                {
                    PartId = value;
                }
            }
        }

        [Required]
        public Order Order { get; set; }

        [Required]
        public int OrderId { get; set; }
    }
}

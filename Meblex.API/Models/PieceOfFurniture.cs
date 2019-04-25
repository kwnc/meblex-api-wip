using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meblex.API.Models
{
    public class PieceOfFurniture
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PieceOfFurnitureId { get; set; }

        [Required]
        [StringLength(32)]
        public string Name { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        [StringLength(32)]
        public string Size { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual List<CustomSizeForm> CustomSizeForms { get; set; }

        [Required]
        public virtual Room Room { get; set; }

        [Required]
        public int RoomId { get; set; }

        [Required]
        public virtual List<Photo> Photos { get; set; }

        [Required]
        public virtual Category Category { get; set; }

        [Required]
        public int CategoryId { get; set; }

        
        public virtual List<Part> Parts { get; set; }

        public virtual List<OrderLine> OrderLines { get; set; }
    }
}

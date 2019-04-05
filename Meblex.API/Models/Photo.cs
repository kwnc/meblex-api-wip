using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meblex.API.Models
{
    public class Photo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PhotoId { get; set; }

        [Required]
        [StringLength(132)]
        public string Path { get; set; }

        [Required]
        public PieceOfFurniture PieceOfFurniture { get; set; }

        [Required]
        public int PieceOfFurnitureId { get; set; }

    }
}

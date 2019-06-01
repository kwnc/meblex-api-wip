using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meblex.API.Models
{
    public class CustomSizeForm
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomSizeFormId { get; set; }

        [Required]
        [StringLength(32)]
        public string Size { get; set; }

        [Required]
        public bool Approved { get; set; }

        [Required]
        public virtual Client Client { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public virtual PieceOfFurniture PieceOfFurniture { get; set; }

        [Required]
        public int PieceOfFurnitureId { get; set; }
    }
}
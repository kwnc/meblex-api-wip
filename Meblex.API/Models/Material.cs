using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meblex.API.Models
{
    public class Material
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaterialId { get; set; }

        [Required]
        [StringLength(32)]
        public string Name { get; set; }

        [Required]
        [StringLength(32)]
        public string Slug { get; set; }

        public virtual List<Part> Parts { get; set; }

        [Required]
        public virtual MaterialPhoto Photo { get; set; }
        public virtual List<PieceOfFurniture> PieceOfFurniture { get; set; }
    }
}

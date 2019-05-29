using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Meblex.API.Models
{
    public class MaterialPhoto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaterialPhotoId { get; set; }

        [Required]
        [StringLength(132)]
        public string Path { get; set; }

        [Required]
        public virtual Material Material { get; set; }

        [Required]
        public int MaterialId { get; set; }
    }
}

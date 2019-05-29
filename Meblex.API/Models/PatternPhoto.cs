using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Meblex.API.Models
{
    public class PatternPhoto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PatternPhotoId { get; set; }

        [Required]
        [StringLength(132)]
        public string Path { get; set; }

        [Required]
        public virtual Pattern Pattern { get; set; }

        [Required]
        public int PatternId { get; set; }
    }
}

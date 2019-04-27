using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Meblex.API.Models
{
    public class Pattern
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PatternId { get; set; }

        [Required]
        [StringLength(32)]
        public string Name { get; set; }

        [Required]
        [StringLength(32)]
        public string Slug { get; set; }

        public virtual List<Part> Parts { get; set; }
    }
}

﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meblex.API.Models
{
    public class Part
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PartId { get; set; }

        [Required]
        [StringLength(32)]
        public string Name { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public PieceOfFurniture PieceOfFurniture { get; set; }

        [Required]
        public int PieceOfFurnitureId { get; set; }
        [Required]
        public Pattern Pattern { get; set; }
        [Required]
        public int PatternId { get; set; }
        [Required]
        public Color Color { get; set; }
        [Required]
        public int ColorId { get; set; }
        [Required]
        public Material Material { get; set; }
        [Required]
        public int MaterialId { get; set; }
        public OrderLine OrderLine { get; set; }
    }
}

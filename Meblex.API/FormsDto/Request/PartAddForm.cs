using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meblex.API.FormsDto.Request
{
    public class PartAddForm
    {
        public string Name { get; set; }

        public int Count { get; set; }

        public float Price { get; set; }

        public int PieceOfFurnitureId { get; set; }

        public int PatternId { get; set; }

        public int ColorId { get; set; }

        public int MaterialId { get; set; }
    }
}

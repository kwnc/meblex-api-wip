using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meblex.API.FormsDto.Response
{
    public class FurniturePartResponse
    {

        public int PartId { get; set; }

        public string Name { get; set; }

        public int Count { get; set; }

        public float Price { get; set; }

        public virtual PatternsResponse Pattern { get; set; }


        public virtual ColorsResponse Color { get; set; }


        public virtual MaterialResponse Material { get; set; }

    }
}

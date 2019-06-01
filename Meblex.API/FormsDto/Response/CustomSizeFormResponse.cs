using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meblex.API.FormsDto.Response
{
    public class CustomSizeFormResponse
    {
        public string Size { get; set; }

        public bool Approved { get; set; }

        public float Price { get; set; }

        public virtual FurnitureResponse PieceOfFurniture { get; set; }

    }
}

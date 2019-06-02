using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meblex.API.FormsDto.Response
{
    public class OrderLineResponse
    {
        public int OrderLineId { get; set; }

        public int Count { get; set; }

        public int Price { get; set; }

        public string Size { get; set; }


        public virtual ShoppingCartFurnitureResponse PieceOfFurniture { get; set; }


        public virtual ShoppingCartPartResponse Part { get; set; }

    }
}

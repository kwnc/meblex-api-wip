using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meblex.API.FormsDto.Response
{
    public class ShoppingCartFurnitureResponse
    {
        public int PieceOfFurnitureId { get; set; }
        public string Name { get; set; }
        public List<string> Photos { get; set; }
    }
}

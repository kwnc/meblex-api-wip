using System.Collections.Generic;

namespace Meblex.API.DTO
{
    public class PieceOfFurnitureAddDto
    {
        public PieceOfFurnitureAddDto()
        {
            PartsId = new List<int>();
        }
        public string Name { get; set; }

        public int Count { get; set; }

        public double Price { get; set; }

        public string Size { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }
        public int RoomId { get; set; }

        public List<int> PartsId { get; set; }
    }
}

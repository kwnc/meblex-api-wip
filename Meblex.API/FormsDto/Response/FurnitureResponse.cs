using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meblex.API.FormsDto.Response
{
    public class FurnitureResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Count { get; set; }

        public double Price { get; set; }

        public string Size { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }
        public int RoomId { get; set; }

        public List<FurniturePartResponse> Parts { get; set; }

        public List<string> PhotoNames { get; set; }
    }
}

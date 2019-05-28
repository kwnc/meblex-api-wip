using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meblex.API.FormsDto.Response
{
    public class ColorsResponse
    {
        public int ColorId { get; set; }

        public string Name { get; set; }

        public string HexCode { get; set; }

        public string Slug { get; set; }
    }
}

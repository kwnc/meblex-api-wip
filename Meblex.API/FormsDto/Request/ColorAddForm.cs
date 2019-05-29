using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meblex.API.FormsDto.Request
{
    public class ColorAddForm
    {
        public string Name { get; set; }

        public string HexCode { get; set; }

        public string Slug { get; set; }
    }
}

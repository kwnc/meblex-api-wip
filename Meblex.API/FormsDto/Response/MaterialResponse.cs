using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Meblex.API.FormsDto.Response
{
    public class MaterialResponse
    {
        public int MaterialId { get; set; }

        public string Name { get; set; }
        public string Slug { get; set; }

        public string Photo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meblex.API.FormsDto.Response
{
    public class AuthLoginResponse
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public int PostCode { get; set; }

        public int NIP { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}

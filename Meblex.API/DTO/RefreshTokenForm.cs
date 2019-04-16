using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Meblex.API.Validation;

namespace Meblex.API.DTO
{
    public class RefreshTokenForm
    {
        [Required, RefreshTokenValidation]
        public string Token { get; set; }
    }
}

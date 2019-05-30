using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Meblex.API.Context;
using Meblex.API.Interfaces;

namespace Meblex.API.Services
{
    public class FitterService:IFitterService
    {
        private readonly MeblexDbContext _context;

        public FitterService(MeblexDbContext context)
        {
            _context = context;
        }

    }
}

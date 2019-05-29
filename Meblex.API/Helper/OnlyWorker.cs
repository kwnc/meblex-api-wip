using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Meblex.API.Helper
{
    public class OnlyWorker:AuthorizeAttribute
    {

    }
}

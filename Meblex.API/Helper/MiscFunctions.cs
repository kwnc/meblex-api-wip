using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NJsonSchema;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Meblex.API.Helper
{
    public  class MiscFunctions
    {
        public class CustomModelDocumentFilter<T> : IDocumentFilter where T : class
        {
            public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
            {
                context.SchemaRegistry.GetOrRegister(typeof(T));
            }
        }
    }
}

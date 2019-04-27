using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Meblex.API.Helper
{
    public class ExceptionDetails
    {
        public string Error { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public string TraceId { get; set; }

        public override string ToString()
        {
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            return JsonConvert.SerializeObject(this, serializerSettings);
        }
    }
}

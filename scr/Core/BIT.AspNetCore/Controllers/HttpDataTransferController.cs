using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BIT.Data.Transfer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BIT.AspNetCore.Controllers
{
    public class HttpDataTransferController:BaseController
    {
        [HttpPost]
        public virtual async Task<DataResult> Post()
        {
            throw new NotImplementedException();
            //var stream = Request.Body;

            //var result = DeserializeFromStream(stream);
            ////[FromBody] Parameters dataparameters
            ////return File(stream, "application/octet-stream");
            //var Errors = new List<string>();
            //Errors.Add("Error1");
            //Errors.Add("Error2");
            //Errors.Add("Error3");
            //return new DataResult() { Errors = Errors };
        }
        protected virtual DataParameters DeserializeFromStream(Stream stream)
        {
            var serializer = new JsonSerializer();

            using (var sr = new StreamReader(stream))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                return serializer.Deserialize<DataParameters>(jsonTextReader);
            }
        }
    }
}

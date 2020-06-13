using BIT.AspNetCore.Controllers;
using BIT.Data.Transfer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HttpDataTransferTestController : HttpDataTransferController
    {
        public HttpDataTransferTestController()
        {
        }
        public async override Task<DataResult> Post()
        {
            var stream = Request.BodyReader.AsStream();

            DataParameters result = DeserializeFromStream(stream);

          
            //[FromBody] Parameters dataparameters
            //return File(stream, "application/octet-stream");
            var Errors = new List<string>();
            Errors.Add("Error1");
            Errors.Add("Error2");
            Errors.Add("Error3");
            return new DataResult() { Errors = Errors };
        }

    }
}

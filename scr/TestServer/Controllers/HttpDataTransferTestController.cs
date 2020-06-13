using BIT.AspNetCore.Controllers;
using BIT.Data.Helpers;
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
        IObjectSerializationHelper _ObjectSerializationHelper;
        public HttpDataTransferTestController(IObjectSerializationHelper ObjectSerializationHelper)
        {
            this.ObjectSerializationHelper = ObjectSerializationHelper;
        }

        public IObjectSerializationHelper ObjectSerializationHelper { get => _ObjectSerializationHelper; set => _ObjectSerializationHelper = value; }

        public async override Task<DataResult> Post()
        {
            //TODO check why the serialization does not work when the class DataResult inherits from dictionary
            var stream = Request.BodyReader.AsStream();

            DataParameters result = DeserializeFromStream(stream);

            byte[] v = ObjectSerializationHelper.ToByteArray<string>("Hello Data Transfer");
            var Errors = new List<string>();
            Errors.Add("Error1");
            Errors.Add("Error2");
            Errors.Add("Error3");

            string v1 = Convert.ToBase64String(v);
            switch (result.MemberName)
            {
                case "NoErrors":

                  
                    return new DataResult(null, v) { ResultValue2 = v1 };
                    
                case "Errors":

                    return new DataResult(Errors, v) { ResultValue2 = v1 };



            }
            return new DataResult(Errors, v) { ResultValue2 = v1 };

        }

    }
}

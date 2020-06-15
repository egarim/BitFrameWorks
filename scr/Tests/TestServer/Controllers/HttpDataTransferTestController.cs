using BIT.AspNetCore.Controllers;
using BIT.Data.DataTransfer;
using BIT.Data.Services;
using BIT.Xpo.Providers.WebApi.Server;
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
        IObjectSerializationService _ObjectSerializationHelper;
        public HttpDataTransferTestController(IObjectSerializationService ObjectSerializationHelper)
        {
            this.ObjectSerializationHelper = ObjectSerializationHelper;
        }

        public IObjectSerializationService ObjectSerializationHelper { get => _ObjectSerializationHelper; set => _ObjectSerializationHelper = value; }

        public async override Task<IDataResult> Post()
        {
            //TODO check why the serialization does not work when the class DataResult inherits from dictionary
            var stream = Request.BodyReader.AsStream();

            var result = await DeserializeFromStream(stream);

            byte[] v = ObjectSerializationHelper.ToByteArray<string>("Hello Data Transfer");
            var Errors = new Dictionary<string,string>();
            Errors.Add("1","Error1");
            Errors.Add("2", "Error2");
            Errors.Add("3","Error3");

            string v1 = Convert.ToBase64String(v);
            switch (result.MemberName)
            {
                case "NoErrors":


                    DataResult dataResult = new DataResult();
                    //dataResult.Add("aaaa", "abc");
                    dataResult.ResultValue = v;
                    dataResult.Errors = Errors;
                    return dataResult;
                    
                case "Errors":

                    return new DataResult();



            }
            return new DataResult();

        }

    }
}

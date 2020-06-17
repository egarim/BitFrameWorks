using BIT.AspNetCore.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BIT.Xpo.Providers.Network.HttpApiServer
{
    public class WebApiHttpProtobufController: BaseController
    {

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    var person = new Person
        //    {
        //        FirstName = "Sam",
        //        BillingAddress = new Address
        //        {
        //            StreeNumber = "100",
        //            Street = "Somewhere",
        //            Suburb = "Sometown"
        //        },
        //        Surname = "Smith"
        //    };

        //    var data = person.ToByteArray();
        //    return File(data, "application/octet-stream");
        //}

        [HttpPut]
        public async Task<IActionResult> Put()
        {
            //var stream = Request.BodyReader.AsStream();
            //var person = Person.Parser.ParseFrom(stream);
            //if (!Request.Headers.ContainsKey("PersonKey")) throw new Exception("No key");
            //person.PersonKey = Request.Headers["PersonKey"];
            //var data = person.ToByteArray();
            //return File(data, "application/octet-stream");
        }

    }
}

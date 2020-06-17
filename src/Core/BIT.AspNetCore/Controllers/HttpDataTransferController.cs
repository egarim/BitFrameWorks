using BIT.Data.DataTransfer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BIT.AspNetCore.Controllers
{
    public class HttpDataTransferController:BaseController
    {
        [HttpPost]
        public virtual async Task<IDataResult> Post()
        {
            throw new NotImplementedException();
         
        }
        protected async virtual Task<IDataParameters> DeserializeFromStream(Stream stream)
        {
            try
            {
              
                var sr = new StreamReader(stream);
                var json= await  sr.ReadToEndAsync();
                    return JsonConvert.DeserializeObject<DataParameters>(json);
                
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                throw;
            }

        }
        //protected virtual IDataParameters DeserializeFromStream(Stream stream)
        //{
        //    try
        //    {
        //        var serializer = new JsonSerializer();

        //        using (var sr = new StreamReader(stream))
        //        using (var jsonTextReader = new JsonTextReader(sr))
        //        {
        //            return serializer.Deserialize<DataParameters>(jsonTextReader);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var message = ex.Message;
        //        throw;
        //    }
          
        //}
    }
}

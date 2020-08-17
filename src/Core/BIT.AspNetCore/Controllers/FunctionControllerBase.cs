using BIT.Data.Functions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BIT.AspNetCore.Controllers
{
    public abstract class FunctionControllerBase:BaseController
    {
        [HttpPost]
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public virtual async Task<IDataResult> Post()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            throw new NotImplementedException();
         
        }
        protected async virtual Task<IDataParameters> DeserializeFromStream(Stream stream)
        {
            var sr = new StreamReader(stream);
            var json = await sr.ReadToEndAsync();
            return JsonConvert.DeserializeObject<DataParameters>(json);

        }
      
    }
}

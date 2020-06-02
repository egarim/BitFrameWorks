using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BIT.Xpo.AspNetCore;
using BIT.Data.Models;

namespace BIT.Xpo.Providers.WebApi.Server
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authenticate]
    public class XPOWebApiControllerBase : BaseController
    {
        protected const string DataStoreIdHeader = "DataStoreId";
        protected IDataStoreResolver _Resolver;
      
        public XPOWebApiControllerBase(IDataStoreResolver DataStoreResolver)
        {
            _Resolver = DataStoreResolver;
        }
        [HttpPost]
        [Route("[action]")]

        public virtual byte[] GetAutoCreateOptions()
        {
            return Utilities.ToByteArray(_Resolver.GetDataStore(GetHeader(DataStoreIdHeader)).AutoCreateOption);


        }
        [HttpPost]
        [Route("[action]")]

        public virtual async Task<IActionResult> SelectData()
        {
            try
            {
                byte[] Bytes = null;
                Bytes = await Request.GetRawBodyBytesAsync();
                SelectedData SelectedData =
                    _Resolver.GetDataStore(GetHeader(DataStoreIdHeader)).
                    SelectData(Utilities.GetObjectsFromByteArray<SelectStatement[]>(Bytes));
                return Ok(Utilities.ToByteArray(SelectedData));
            }
            catch (Exception ex)
            {

                return BadRequest(new ApiExceptionBase(ex));
            }

        }
        [HttpPost]
        [Route("[action]")]
        public virtual async Task<IActionResult> Do()
        {

           
            try
            {
                byte[] Bytes = null;
                Bytes = await Request.GetRawBodyBytesAsync();
                var Parameters = Utilities.GetObjectsFromByteArray<string>(Bytes);

                var Values = JsonConvert.DeserializeObject<Dictionary<string, object>>(Parameters);

                var DataStore = _Resolver.GetDataStore(GetHeader(DataStoreIdHeader));
               
                var Result = (DataStore as ICommandChannel).Do(Values["Command"].ToString(), Values["Args"]);
                return Ok(Utilities.ToByteArray(Result));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiExceptionBase(ex));
            }

        }
        [HttpPost]
        [Route("[action]")]
        public virtual async Task<IActionResult> ModifyData()
        {

            byte[] Bytes = null;
            try
            {
                Bytes = await Request.GetRawBodyBytesAsync();

                var DmlString = Utilities.GetObjectsFromByteArray<ModificationStatement[]>(Bytes);
                var Result = _Resolver.GetDataStore(GetHeader(DataStoreIdHeader)).
                    ModifyData(DmlString);
                return Ok(Utilities.ToByteArray(Result));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiExceptionBase(ex));
            }

        }

        [HttpPost]
        [Route("[action]")]
        public virtual async Task<IActionResult> UpdateSchema()
        {
            try
            {
                byte[] Bytes = null;
                Bytes = await Request.GetRawBodyBytesAsync();
                var Parameters = Utilities.GetObjectsFromByteArray<string>(Bytes);

                var Values = JsonConvert.DeserializeObject<Dictionary<string, object>>(Parameters);
               

                DBTable[] tables = Utilities.DeserializeObjectFromString<DBTable[]>(Values["tables"].ToString());
                UpdateSchemaResult updateSchemaResult = _Resolver.GetDataStore(GetHeader(DataStoreIdHeader)).UpdateSchema(((bool)Values["dontCreateIfFirstTableNotExist"]), tables);
                return Ok(updateSchemaResult);
            }
            catch (Exception ex)
            {

                return BadRequest(new ApiExceptionBase(ex));

            }


        }
    }
}

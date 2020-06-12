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
using BIT.AspNetCore.Controllers;
using BIT.Data.Helpers;
using BIT.AspNetCore.Extensions;

namespace BIT.Xpo.Providers.WebApi.Server
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authenticate]
    public class XPOWebApiControllerBase : BaseController
    {
        protected const string DataStoreIdHeader = "DataStoreId";
        private IConfigResolver<IDataStore> resolver;
        private IObjectSerializationHelper iObjectSerializationHelper;
        private IStringSerializationHelper iStringSerializationHelper;


        public IStringSerializationHelper StringSerializationHelper { get => iStringSerializationHelper; protected set => iStringSerializationHelper = value; }
        public IObjectSerializationHelper ObjectSerializationHelper { get => iObjectSerializationHelper; protected set => iObjectSerializationHelper = value; }
        public IConfigResolver<IDataStore> Resolver { get => resolver; protected set => resolver = value; }

        public XPOWebApiControllerBase(IConfigResolver<IDataStore> DataStoreResolver,IObjectSerializationHelper objectSerializationHelper, IStringSerializationHelper stringSerializationHelper)
        {
            Resolver = DataStoreResolver;
            ObjectSerializationHelper = objectSerializationHelper;
            StringSerializationHelper = stringSerializationHelper;
        }
        [HttpPost]
        [Route("[action]")]

        public virtual byte[] GetAutoCreateOptions()
        {
            return iObjectSerializationHelper.ToByteArray(Resolver.GetById(GetHeader(DataStoreIdHeader)).AutoCreateOption);


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
                    Resolver.GetById(GetHeader(DataStoreIdHeader)).
                    SelectData(iObjectSerializationHelper.GetObjectsFromByteArray<SelectStatement[]>(Bytes));
                return Ok(iObjectSerializationHelper.ToByteArray(SelectedData));
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
                var Parameters = iObjectSerializationHelper.GetObjectsFromByteArray<string>(Bytes);

                var Values = JsonConvert.DeserializeObject<Dictionary<string, object>>(Parameters);

                var DataStore = Resolver.GetById(GetHeader(DataStoreIdHeader));
               
                var Result = (DataStore as ICommandChannel).Do(Values["Command"].ToString(), Values["Args"]);
                return Ok(iObjectSerializationHelper.ToByteArray(Result));
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

                var DmlString = iObjectSerializationHelper.GetObjectsFromByteArray<ModificationStatement[]>(Bytes);
                var Result = Resolver.GetById(GetHeader(DataStoreIdHeader)).
                    ModifyData(DmlString);
                return Ok(iObjectSerializationHelper.ToByteArray(Result));
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
                var Parameters = ObjectSerializationHelper.GetObjectsFromByteArray<string>(Bytes);

                var Values = JsonConvert.DeserializeObject<Dictionary<string, object>>(Parameters);
               

                DBTable[] tables = StringSerializationHelper.DeserializeObjectFromString<DBTable[]>(Values["tables"].ToString());
                UpdateSchemaResult updateSchemaResult = Resolver.GetById(GetHeader(DataStoreIdHeader)).UpdateSchema(((bool)Values["dontCreateIfFirstTableNotExist"]), tables);
                return Ok(updateSchemaResult);
            }
            catch (Exception ex)
            {

                return BadRequest(new ApiExceptionBase(ex));

            }


        }
    }
}

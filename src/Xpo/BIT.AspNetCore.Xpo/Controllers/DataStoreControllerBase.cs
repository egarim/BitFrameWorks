
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BIT.AspNetCore.Controllers;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BIT.AspNetCore.Extensions;
using BIT.Data.Services;
using BIT.Xpo.Models;

namespace BIT.Xpo.AspNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class DataStoreControllerBase : BaseController
    {
        protected IDataStore dataStore;
        protected IObjectSerializationService objectSerializationService;

        public DataStoreControllerBase(IDataStore dataStore, IObjectSerializationService objectSerializationService)
        {
            this.dataStore = dataStore;
            this.objectSerializationService = objectSerializationService;
        }
        [HttpPost]
        [Route("[action]")]

        public virtual byte[] GetAutoCreateOptions()
        {
            return objectSerializationService.ToByteArray(dataStore.AutoCreateOption);

        }
        [HttpPost]
        [Route("[action]")]

        public virtual async Task<IActionResult> SelectData()
        {
            try
            {
                byte[] Bytes = null;
                Bytes = await Request.GetRawBodyBytesAsync();
                SelectedData SelectedData = dataStore.SelectData(objectSerializationService.GetObjectsFromByteArray<SelectStatement[]>(Bytes));
                return  Ok(objectSerializationService.ToByteArray(SelectedData));
            }
            catch (Exception ex)
            {
                
               return BadRequest(ex); 
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

                var Result = dataStore.ModifyData(objectSerializationService.GetObjectsFromByteArray<ModificationStatement[]>(Bytes));
                return Ok(objectSerializationService.ToByteArray(Result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
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
                var Parameters = objectSerializationService.GetObjectsFromByteArray<UpdateSchemaParameters>(Bytes);
                UpdateSchemaResult updateSchemaResult = dataStore.UpdateSchema(Parameters.dontCreateIfFirstTableNotExist, Parameters.tables);
                return Ok(updateSchemaResult);
            }
            catch (Exception ex)
            {

                Debug.WriteLine(string.Format("{0}:{1}", "exception.Message", ex.Message));
                if (ex.InnerException != null)
                {
                    Debug.WriteLine(string.Format("{0}:{1}", "exception.InnerException.Message", ex.InnerException.Message));

                }
                Debug.WriteLine(string.Format("{0}:{1}", " exception.StackTrace", ex.StackTrace));
                return BadRequest(ex);
            }
          
           
        }
    }
}
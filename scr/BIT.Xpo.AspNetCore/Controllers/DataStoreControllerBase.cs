
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BIT.AspNetCore.Controllers;
using BIT.Data.Models;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BIT.Xpo.AspNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class DataStoreControllerBase : BaseController
    {
        protected IDataStore _DataStore;

        public DataStoreControllerBase(IDataStore DataStore)
        {
            _DataStore = DataStore;
        }
        [HttpPost]
        [Route("[action]")]

        public virtual byte[] GetAutoCreateOptions()
        {
            return Utilities.ToByteArray(_DataStore.AutoCreateOption);

        }
        [HttpPost]
        [Route("[action]")]

        public virtual async Task<IActionResult> SelectData()
        {
            try
            {
                byte[] Bytes = null;
                Bytes = await Request.GetRawBodyBytesAsync();
                SelectedData SelectedData = _DataStore.SelectData(Utilities.GetObjectsFromByteArray<SelectStatement[]>(Bytes));
                return  Ok(Utilities.ToByteArray(SelectedData));
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

                var Result = _DataStore.ModifyData(Utilities.GetObjectsFromByteArray<ModificationStatement[]>(Bytes));
                return Ok(Utilities.ToByteArray(Result));
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
                var Parameters = Utilities.GetObjectsFromByteArray<UpdateSchemaParameters>(Bytes);
                UpdateSchemaResult updateSchemaResult = _DataStore.UpdateSchema(Parameters.dontCreateIfFirstTableNotExist, Parameters.tables);
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
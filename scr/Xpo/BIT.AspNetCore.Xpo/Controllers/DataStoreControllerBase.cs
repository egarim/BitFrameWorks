
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
using BIT.Data.Xpo.Models;

namespace BIT.Xpo.AspNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class DataStoreControllerBase : BaseController
    {
        protected IDataStore _DataStore;
        protected IObjectSerializationService _objectSerializationHelper;

        public DataStoreControllerBase(IDataStore DataStore, IObjectSerializationService objectSerializationHelper)
        {
            _DataStore = DataStore;
            _objectSerializationHelper = objectSerializationHelper;
        }
        [HttpPost]
        [Route("[action]")]

        public virtual byte[] GetAutoCreateOptions()
        {
            return _objectSerializationHelper.ToByteArray(_DataStore.AutoCreateOption);

        }
        [HttpPost]
        [Route("[action]")]

        public virtual async Task<IActionResult> SelectData()
        {
            try
            {
                byte[] Bytes = null;
                Bytes = await Request.GetRawBodyBytesAsync();
                SelectedData SelectedData = _DataStore.SelectData(_objectSerializationHelper.GetObjectsFromByteArray<SelectStatement[]>(Bytes));
                return  Ok(_objectSerializationHelper.ToByteArray(SelectedData));
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

                var Result = _DataStore.ModifyData(_objectSerializationHelper.GetObjectsFromByteArray<ModificationStatement[]>(Bytes));
                return Ok(_objectSerializationHelper.ToByteArray(Result));
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
                var Parameters = _objectSerializationHelper.GetObjectsFromByteArray<UpdateSchemaParameters>(Bytes);
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
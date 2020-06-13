using BIT.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Versioning;

namespace BIT.AspNetCore.Controllers
{
    public class TestControllerBase : ControllerBase
    {

        /// <summary>
        /// Use this action to make sure your server is working
        /// </summary>
        // GET api/values
        Stats stats;
        string osPlatform;
        [HttpGet]
        public virtual ActionResult<IEnumerable<string>> Get()
        {
            string framework = "";
            try
            {

                framework = Assembly
                    .GetEntryAssembly()?
                    .GetCustomAttribute<TargetFrameworkAttribute>()?
                    .FrameworkName;
                osPlatform = System.Runtime.InteropServices.RuntimeInformation.OSDescription;
                stats = new Stats
                {
                    OsPlatform = System.Runtime.InteropServices.RuntimeInformation.OSDescription,
                    AspDotnetVersion = framework
                };
            }
            catch (Exception)
            {

                throw;
            }
            //Español,Russian,Italian,French & Rommanian,French,Swedish,German
            return new string[] { "Bit Frameworks", "Hola,Привет,Hello,Ciao,Salut,Oi,Hej,Hallo", "AspNetCore", $"Framework:{framework} platform:{osPlatform}" };
        }

      
    }
}
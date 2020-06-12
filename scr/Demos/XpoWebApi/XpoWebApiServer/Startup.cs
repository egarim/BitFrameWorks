using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BIT.Data.Helpers;
using BIT.Xpo;
using BIT.Xpo.Providers.WebApi.Server;
using DevExpress.Xpo.DB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace XpoWebApiServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();


            //HACK how to add XpoWebApi

            #region Manually register all services needed for XpoWebApi

            //IResolver<IDataStore> DataStoreResolver = new XpoDataStoreResolver("appsettings.json");
            //IStringSerializationHelper stringSerializationHelper = new StringSerializationHelper();
            //IObjectSerializationHelper objectSerializationHelper = new SimpleObjectSerializationHelper();
            //services.AddSingleton<IResolver<IDataStore>>(DataStoreResolver);
            //services.AddSingleton<IStringSerializationHelper>(stringSerializationHelper);
            //services.AddSingleton<IObjectSerializationHelper>(objectSerializationHelper);


            #endregion

            //OR use the extension method AddXpoWebApi overload

            services.AddXpoWebApi();




            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

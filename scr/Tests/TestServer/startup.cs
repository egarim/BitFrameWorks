using BIT.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using DevExpress.Xpo.DB;
using BIT.Data.Xpo;

namespace TestServer
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
            IConfigResolver<IDataStore> DataStoreResolver = new XpoDataStoreResolver("appsettings.json");
            IStringSerializationHelper stringSerializationHelper = new StringSerializationHelper();
            IObjectSerializationService objectSerializationHelper = new SimpleObjectSerializationService();
            services.AddSingleton<IConfigResolver<IDataStore>>(DataStoreResolver);
            services.AddSingleton<IStringSerializationHelper>(stringSerializationHelper);
            services.AddSingleton<IObjectSerializationService>(objectSerializationHelper);
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

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using BIT.Xpo.Providers.WebApi.Client;
using BIT.Xpo;
using XpoDemoOrm;

namespace Blazorwasm
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            XpoWebApiProvider.Register();

            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddBaseAddressHttpClient();

            await builder.Build().RunAsync();
        }
    }
}

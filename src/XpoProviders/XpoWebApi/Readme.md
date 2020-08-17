### XpoWebApi
---

XpoWebApi is an open source XPO provider that allow your application to communicate to a data store hosted in a Web API




### AspNetCore Getting Started

#### Server

1. Create an AspNetCore Web Api project
2. Add references to the nuget **BIT.Xpo.Providers.WebApi.AspNetCore**
3. Add XpoWebApi to the list of services in your startup class

```<language>
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();
    services.AddXpoWebApi();
}
```
4. Implement a controller that inherit from XpoWebApiControllerBase

```<language>
using BIT.Data.DataTransfer;
using BIT.Xpo.Providers.WebApi.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace XpoWebApiDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class XpoWebApiController : XpoWebApiControllerBase
    {
        public XpoWebApiController(IFunction DataStoreFunctionServer) : base(DataStoreFunctionServer)
        {
        }
    }
}

```
5. Setup your connection strings in your appsettings.json, for each entry on the ConnectionStrings section
there should be matching entry on the DatabaseAutoCreateOptions section

```<language>
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "ConnectionString": "XpoProvider=InMemoryDataStore;case sensitive=False",
    "001": "XpoProvider=InMemoryDataStore;case sensitive=False"
  
  

  },
  "DatabaseAutoCreateOptions": {
    "ConnectionString": "DatabaseAndSchema",
    "001": "DatabaseAndSchema"
  
  }
}

```
6. Test your setup, navigate to the url of the XpoWebApi controller that you implemented on step 4, the url should be something like https://localhost/api/XpoWebApi, this user might be different depending on your setup, you should see a message like this:

   "This is working )) YourProjectName.Controllers.XpoWebApiController"

#### Client

1. Add reference to XpoWebApi client nuget **BIT.Xpo.Providers.WebApi.Client**
2. Register our provider as soon as you can in your application lifetime by using the following line:

```<language>

   XpoWebApiProvider.Register();

```
3. Use the following connection string in your application

```<language>
XpoProvider=XpoWebApiProvider;Url=UrlOfYourApi;Controller=ControllerPath;Token=YourTokenIfYouHaveOne;DataStoreId=TheIdOfYourDataStore
```

You can also use the static method GetConnectionString of the XpoWebApiProvider class as shown in the example below 
```<language>

   var XpoWebApiAspNetCore = XpoWebApiProvider.GetConnectionString("https://localhost:44359", "/XpoWebApi", string.Empty, DataStoreId);

```
### AspNet Getting Started

Coming soon

### Videos on youtube

1. XpoWebApi for AspNetCore Getting Started https://youtu.be/2IJOKE7yfr0
2. XpoWebApi for AspNetCore Using Multiple DataStore https://youtu.be/RTDg4fZL-5g
3. Using unsupported databases in netcore through XpoWebApi https://youtu.be/2eJrqPui9Sk

### Examples

1. XpoWebApi for AspNetCore Getting Started https://github.com/egarim/XpoWebApi-for-AspNetCore-GettingStarted
2. XpoWebApi for AspNetCore Using Multiple DataStore https://github.com/egarim/XpoWebApi-for-AspNetCore-Using-Multiple-DataStore
3. Using unsupported databases in netcore through XpoWebApi https://github.com/egarim/Using-unsupported-databases-in-netcore-through-XpoWebApi






using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Cosmonaut;
using Cosmonaut.Extensions.Microsoft.DependencyInjection;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.DependencyInjection;

namespace BIT.SingularOrm.Cosmonaut
{
    public class CosmonautDataSpaceProvider : DataSpaceProvider
    {
        private readonly string DatabaseId;
        private readonly Dictionary<Type, object> stores = new Dictionary<Type, object>();

        //public CosmonautDataSpaceProvider(string DatabaseId, IBrevitasAppConfiguration appConfiguration,
        //    string httpslocalhost8081, string AuthKey, IEnumerable<Type> Entities) : base(appConfiguration)
        //{
        //    this.DatabaseId = DatabaseId;
        //    //https://github.com/Elfocrash/Cosmonaut/tree/develop/samples/Cosmonaut.Console
        //    var connectionPolicy = new ConnectionPolicy
        //    {
        //        ConnectionProtocol = Protocol.Tcp,
        //        ConnectionMode = ConnectionMode.Direct
        //    };

        //    var cosmosSettings = new CosmosStoreSettings(DatabaseId,
        //        httpslocalhost8081,
        //        AuthKey
        //        , connectionPolicy
        //        , defaultCollectionThroughput: 5000);


        //    var serviceCollection = new ServiceCollection();


        //    var ExtensionMethods = typeof(ServiceCollectionExtensions).GetMethods();

        //    //var Method = ExtensionMethods.Where(m => m.Name == "AddCosmosStore").FirstOrDefault();
        //    var Method = ExtensionMethods.Where(m => m.Name == nameof(ServiceCollectionExtensions.AddCosmosStore))
        //        .FirstOrDefault();
        //    foreach (var type in Entities) AddCosmosStore(cosmosSettings, serviceCollection, Method, type);
        //    var provider = serviceCollection.BuildServiceProvider();
        //    var GenericType = typeof(ICosmosStore<>);
        //    var NewTypes = new Dictionary<Type, object>();
        //    foreach (var type in Entities)
        //    {
        //        var makeme = GenericType.MakeGenericType(type);
        //        var value = provider.GetService(makeme);
        //        stores.Add(type, value);
        //        NewTypes.Add(type, value);
        //    }
        //}


        public static void KeepReference()
        {
        }

        private static void AddCosmosStore(CosmosStoreSettings cosmosSettings, ServiceCollection serviceCollection,
            MethodInfo CreateListInternalMethod, Type ObjectType)
        {
            //this method is actually an extension,https://github.com/Elfocrash/Cosmonaut/blob/develop/src/Cosmonaut.Extensions.Microsoft.DependencyInjection/ServiceCollectionExtensions.cs
            var generic = CreateListInternalMethod.MakeGenericMethod(ObjectType);
            generic.Invoke(serviceCollection, new object[3] {serviceCollection, cosmosSettings, ""});
        }

        public override IDataSpace CreateDataSpace()
        {
            //TODO fix
            throw new NotImplementedException();
            //return new CosmonautDataSpaceBase(AppConfiguration, stores);
        }

        //public override void CreateSchema()
        //{
        //    base.CreateSchema();
        //}

        protected override void OnSchemaMismatch(IDataSpaceProvider source, SchemaMismatchEventArgs e)
        {
            base.OnSchemaMismatch(source, e);
        }
    }
}
using BIT.Data.Services;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BIT.Xpo
{
    public class XpoInitializerResolver: IResolver<IXpoInitializer>
    {
        Func<string, IResolver<IDataStore>, IXpoInitializer> _DalBuilder;
        IResolver<IDataStore> _DataStoreResolver;
       
        public XpoInitializerResolver(IResolver<IDataStore> DataStoreResolver):this(DataStoreResolver,null)
        {

            this._DalBuilder = new Func<string, IResolver<IDataStore>, IXpoInitializer>(Build);
        }
        public XpoInitializerResolver(IResolver<IDataStore> DataStoreResolver, Func<string, IResolver<IDataStore>, IXpoInitializer> DalBuilder)
        {
            _DalBuilder = DalBuilder;
            _DataStoreResolver = DataStoreResolver;

        }

        private IXpoInitializer Build(string DataStoreId, IResolver<IDataStore> DataStoreResolver)
        {
            return new XpoInitializer(DataStoreResolver.GetById(DataStoreId), null);
        }

        public IXpoInitializer GetById(string Id)
        {
            return this._DalBuilder(Id, this._DataStoreResolver);
        }
    }
}

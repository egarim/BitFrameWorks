using BIT.Data.Helpers;
using BIT.Data.Transfer;
using DevExpress.Xpo.DB;
using System;

namespace BIT.Xpo.Providers.Network.Client.RestClientNet
{
    public class RestClientNetProvider : NetworkClientProviderBase
    {
        public RestClientNetProvider(IFunctionClient functionClient, IObjectSerializationHelper objectSerializationHelper, AutoCreateOption autoCreateOption) : base(functionClient, objectSerializationHelper, autoCreateOption)
        {
        }
    }
}

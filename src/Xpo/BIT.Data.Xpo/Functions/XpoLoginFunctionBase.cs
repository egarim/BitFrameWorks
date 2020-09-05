using BIT.Data.Functions;
using BIT.Data.Models;
using BIT.Data.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BIT.Xpo.Functions
{
    public abstract class XpoLoginFunctionBase : LoginFunctionBase
    {
        protected IResolver<IXpoInitializer> XpoInitializerResolver;

        public XpoLoginFunctionBase(IObjectSerializationService objectSerializationService, ITokenBuilder tokenBuilder, IResolver<IXpoInitializer> XpoInitializerResolver) : base(objectSerializationService, tokenBuilder)
        {
            this.XpoInitializerResolver = XpoInitializerResolver;
        }
        //public XpoLoginFunctionBase(IObjectSerializationService objectSerializationService, IResolver<IXpoInitializer> XpoInitializerResolver, IResolver<object> TokenBuilder) : base(objectSerializationService)
        //{
           
        //}
    }
}

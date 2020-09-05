using BIT.Data.Models;
using BIT.Data.Services;
using System;

namespace BIT.Data.Functions
{
    public abstract class LoginFunctionBase : IFunction
    {


        protected IObjectSerializationService objectSerializationService;
        protected ITokenBuilder tokenBuilder;
        public LoginFunctionBase(IObjectSerializationService objectSerializationService, ITokenBuilder tokenBuilder)
        {

            this.objectSerializationService = objectSerializationService;
            this.tokenBuilder = tokenBuilder;
        }
        public abstract ApiAuthenticationResult Authenticate(LoginParameters LoginParameters);

        public virtual IDataResult ExecuteFunction(IDataParameters Parameters)
        {
            DataResult dataResult = new DataResult();
            LoginParameters LoginParameters = objectSerializationService.GetObjectsFromByteArray<LoginParameters>(Parameters.ParametersValue);
            dataResult.ResultValue = objectSerializationService.ToByteArray(this.Authenticate(LoginParameters));
            return dataResult;

        }
    }
}

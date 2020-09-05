using BIT.Data.Models;
using BIT.Data.Services;
using BIT.Xpo;
using BIT.Xpo.Functions;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using System;

namespace BIT.Xaf
{
    public class XafXpoLoginFunction : XpoLoginFunctionBase
    {
        private const string XafId = "XafId";

        public XafXpoLoginFunction(IObjectSerializationService objectSerializationService, ITokenBuilder tokenBuilder, IResolver<IXpoInitializer> XpoInitializerResolver) : base(objectSerializationService, tokenBuilder, XpoInitializerResolver)
        {

        }

        public override ApiAuthenticationResult Authenticate(LoginParameters LoginParameters)
        {

            ApiAuthenticationResult authenticationResult = new ApiAuthenticationResult();
          
            
            try
            {
                if (!LoginParameters.ContainsValue(XafXpoLoginFunction.XafId))
                {
                    throw new ArgumentException("Missing XafId on LoginParameters");
                }
                var XafDal = this.XpoInitializerResolver.GetById(XafId);
                var UoW = XafDal.CreateUnitOfWork();
                var User = UoW.FindObject<PermissionPolicyUser>(new BinaryOperator("UserName", LoginParameters.Username));

                if (User == null)
                {
                    authenticationResult.LastError = "User not found";
                    return authenticationResult;
                }
                if (!User.ComparePassword(LoginParameters["Password"]?.ToString()))
                {
                    authenticationResult.LastError = "Password do not match";
                    return authenticationResult;
                }

                authenticationResult.Authenticated = true;
                authenticationResult.UserId = User.Oid.ToString();
                authenticationResult.Username = User.UserName;
                authenticationResult.Add("Token", this.tokenBuilder.BuildToken(LoginParameters));
                return authenticationResult;

            }
            catch (Exception exception)
            {
                authenticationResult.LastError = $"{exception.Message}{System.Environment.NewLine}{exception.StackTrace}";

                return authenticationResult;
            }
        }
    }
}

using System;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Data.Filtering;

namespace BIT.Xpo.Functions
{
    
    //public class MyConcatFunction : ICustomFunctionOperatorQueryable, ICustomFunctionOperator, ICustomFunctionOperatorFormattable
    //{
    //    const string FunctionName = nameof(MyConcat);
    //    static readonly MyConcatFunction instance = new MyConcatFunction();
    //    public static void Register()
    //    {
    //        CriteriaOperator.RegisterCustomFunction(instance);
    //    }
    //    public static bool Unregister()
    //    {
    //        return CriteriaOperator.UnregisterCustomFunction(instance);
    //    }

    //    #region ICustomFunctionOperator Members
    //    // Evaluates the function on the client.
    //    public object Evaluate(params object[] operands)
    //    {
          
    //    }
    //    public string Name
    //    {
    //        get { return FunctionName; }
    //    }
    //    public Type ResultType(params Type[] operands)
    //    {
    //        foreach (Type operand in operands)
    //        {
    //            if (operand != typeof(string)) return typeof(object);
    //        }
    //        return typeof(string);
    //    }
    //    #endregion

    //    #region ICustomFunctionOperatorQueryable Members
    //    public System.Reflection.MethodInfo GetMethodInfo()
    //    {
    //        return typeof(MyConcatFunction).GetMethod(FunctionName);
    //    }
    //    #endregion

    //    // The method name must match the function name (specified via FunctionName).
    //    public static string MyConcat(string string1, string string2, string string3, string string4)
    //    {
    //        return (string)instance.Evaluate(string1, string2, string3, string4);
    //    }

    //    public string Format(Type providerType, params string[] operands)
    //    {
    //        //HACK https://docs.devexpress.com/XPO/5206/examples/how-to-implement-a-custom-criteria-language-function-operator

    //        // This example implements the function for MS Access databases only.
    //        if (providerType == typeof(DevExpress.Xpo.DB.MSSqlConnectionProvider))
    //            return string.Format("datepart(\"m\", {0})", operands[0]);

    //        throw new NotSupportedException(string.Concat(
    //            "This provider is not supported: ", providerType.Name));


    //        throw new NotImplementedException();
    //    }
    //}
}
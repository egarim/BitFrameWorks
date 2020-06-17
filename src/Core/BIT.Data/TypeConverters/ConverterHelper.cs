
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIT.Data.TypeConverters
{

    //HACK list of all type converters https://msdn.microsoft.com/en-us/library/system.componentmodel.typeconverter(v=vs.110).aspx
    public class ConverterHelper
    {



        /// <summary>
        /// This function cast the value from the source and convert it to the type of the property
        /// </summary>
        /// <param name="value">the value from the datasource</param>
        /// <param name="itemObjectType">the type of the property (the target type)</param>
        /// <returns>a casted type</returns>
        public virtual object CastValue(object value, Type itemObjectType)
        {
            //HACK TypeConverter info
            //http://msdn.microsoft.com/en-us/library/system.componentmodel.typedescriptor(v=vs.110).aspx

            //HACK how to add an attribute to register a converter
            //http://msdn.microsoft.com/en-us/library/8xazd050(v=vs.110).aspx

            //HACK how to implement your own type converter
            //http://msdn.microsoft.com/en-us/library/ayybcxe5(v=vs.110).aspx

            //HACK List of converters
            //http://msdn.microsoft.com/en-us/library/system.componentmodel.typeconverter(v=vs.110).aspx#inheritanceContinued

            if (value == null)
                return null;

            //TODO find a XAF independent tracing method
            //HACK change tracer texts
            object result = null;
            //Tracing.Tracer.LogText("Obtaining converter");
            //Tracing.Tracer.LogValue("itemObjectType", itemObjectType);
            var converter = TypeDescriptor.GetConverter(itemObjectType);
            //Tracing.Tracer.LogValue("converter", converter);
            //Tracing.Tracer.LogText("Value before convert");
            //Tracing.Tracer.LogValue("value", value);
            result = converter.ConvertFrom(value); 
            //Tracing.Tracer.LogText("Value after convert");
            //Tracing.Tracer.LogValue("value", result);
            return result;
        }





    }


   
}

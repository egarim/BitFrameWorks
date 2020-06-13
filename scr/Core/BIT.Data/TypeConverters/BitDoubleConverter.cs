using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Globalization;

namespace BIT.Data.TypeConverters
{
    public class BitDoubleConverter : DoubleConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(double) || base.CanConvertFrom(context, sourceType);
        }
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(double) || base.CanConvertTo(context, destinationType);
        }
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            //https://msdn.microsoft.com/en-us/library/kfsatb94.aspx convert from a string with R formating 
            return base.ConvertFrom(context, culture, value);

        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
           
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Globalization;

namespace BIT.Data.TypeConverters
{
    public class BitArrayConverter : ArrayConverter
    {
        //HACK http://stackoverflow.com/questions/34330017/convert-string-to-array-of-string-using-typeconverter

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
          return base.CanConvertTo(context, destinationType);
        }
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value!=null)
            {
                return Convert.FromBase64String(value.ToString());
            }
            else
            {
                return null;
            }
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(double))
            {
                return ((DateTime)value).ToOADate();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}

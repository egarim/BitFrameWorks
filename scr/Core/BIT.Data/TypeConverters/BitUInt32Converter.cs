using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Globalization;

namespace BIT.Data.TypeConverters
{
    public class BitUInt32Converter : UInt32Converter
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
            DateTime TryDate = new DateTime();
            DateTime.TryParse(value.ToString(), out TryDate);
            return TryDate;

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

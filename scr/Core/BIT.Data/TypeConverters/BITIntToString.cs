using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace BIT.Data.TypeConverters
{
    public class BITIntToString : StringConverter
    {
        public BITIntToString(Type type) : base()
        {
        }
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType.BaseType == typeof(int) || base.CanConvertFrom(context, sourceType);
        }
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType.BaseType == typeof(int) || base.CanConvertTo(context, destinationType);
        }
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            //HACK this is needed 

            if (value != null)
            {
                if (value.GetType() == typeof(byte[]))
                {
                    return Convert.ToBase64String((byte[])value);
                }
                return value.ToString();
            }
            return base.ConvertFrom(context, culture, value);
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            //hack this i not needed
            if (destinationType == typeof(int))
            {
                return value.ToString();
            }
            
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}

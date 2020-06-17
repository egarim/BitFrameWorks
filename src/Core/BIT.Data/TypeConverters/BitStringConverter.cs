using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Globalization;

namespace BIT.Data.TypeConverters
{
    public class BitStringConverter : StringConverter
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

            if (value != null)
            {
                if (value.GetType() == typeof(bool))
                {
                    return value.ToString();
                }
                if (value.GetType() == typeof(byte))
                {
                    return value.ToString();
                }
                if (value.GetType() == typeof(sbyte))
                {
                    return value.ToString();
                }
                if (value.GetType() == typeof(char))
                {
                    return value.ToString();
                }
                if (value.GetType() == typeof(decimal))
                {
                    return value.ToString();
                }
                if (value.GetType() == typeof(double))
                {
                    string toString = ((double)value).ToString("R");
                    return toString;
                    //return ((double)value).ToString("0." + new string('#', 339));
                    //return value.ToString();
                }
                if (value.GetType() == typeof(Single))
                {
                    return value.ToString();
                }
                if (value.GetType() == typeof(Int16))
                {
                    return value.ToString();
                }
                if (value.GetType() == typeof(UInt16))
                {
                    return value.ToString();
                }
                if (value.GetType() == typeof(Int32))
                {
                    return value.ToString();
                }
                if (value.GetType() == typeof(UInt32))
                {
                    return value.ToString();
                }
                if (value.GetType() == typeof(Int64))
                {
                    return value.ToString();
                }
                if (value.GetType() == typeof(UInt64))
                {
                    return value.ToString();
                }
                if (value.GetType() == typeof(Guid))
                {
                    return value.ToString();
                }
                if (value.GetType().BaseType == typeof(Enum))
                {
                    return Convert.ToInt16(value);
                }
                if (value.GetType() == typeof(string))
                {
                    return value;
                }
                if (value.GetType() == typeof(DateTime))
                {
                    return value.ToString();
                }
                if (value.GetType() == typeof(TimeSpan))
                {
                    return value;
                }
                if (value.GetType() == typeof(byte[]))
                {
                    return Convert.ToBase64String((byte[])value);
                }

            }
            else
            {
                return string.Empty;
            }
            return string.Empty;

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

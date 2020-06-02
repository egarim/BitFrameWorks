using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Globalization;

namespace BIT.Data.TypeConverters
{
    public class BitDateTimeConverter : DateTimeConverter
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
                if ((value.GetType() == typeof(double)) || (value.GetType() == typeof(int)))
                    return DateTime.FromOADate(Convert.ToDouble(value));
                if ((value.GetType() == typeof(string)))
                {
                    Double DateAsDouble = 0;
                    if( Double.TryParse(value.ToString(),out  DateAsDouble))
                    {
                        return DateTime.FromOADate(DateAsDouble);
                    }
                    else
                    {
                        return base.ConvertFrom(context, culture, value);
                    }
                   
                }
                   
                else 
                    return base.ConvertFrom(context, culture, value);

            }
            else
                return null;

        }
     
    }
}

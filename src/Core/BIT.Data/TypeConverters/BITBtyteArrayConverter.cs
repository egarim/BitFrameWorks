//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Globalization;
//using System.Linq;

//namespace BIT.XAF.BaseImpl.TypeConverters
//{
//    public class BITBtyteArrayConverter : ArrayConverter
//    {
//        //HACK http://stackoverflow.com/questions/34330017/convert-string-to-array-of-string-using-typeconverter
//        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
//        {
//            if (sourceType == typeof(byte[]))
//            {
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//            //return base.CanConvertFrom(context, sourceType);
//        }
//        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
//        {
//            if (destinationType == typeof(byte[]))
//            {
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }

//        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
//        {
//            if (value != null)
//            {
//                return Convert.ToBase64String((byte[])value);

//            }
//            return base.ConvertFrom(context, culture, value);
//        }
//    }
//}

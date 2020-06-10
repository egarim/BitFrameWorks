using DevExpress.Xpo.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace BIT.Xpo.Converters
{
    public class TypeToString : ValueConverter
    {
        public override object ConvertFromStorageType(object value)
        {
            string valuestring = value as string;
            if (ReferenceEquals(null, valuestring))
                return null;

            return Type.GetType(valuestring);
        }

        public override object ConvertToStorageType(object value)
        {
            Type CurrentType = (Type)value;
            if (ReferenceEquals(null, CurrentType))
                return typeof(Nullable).ToString();
            return CurrentType.FullName;
        }

        public override Type StorageType
        {
            get { return typeof(string); }
        }
    }
}

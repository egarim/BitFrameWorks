﻿using DevExpress.Xpo.Metadata;
using System;
using System.IO;
using BIT.Data.Helpers;
namespace BIT.Data.Helpers
{
    //HACK todo fix
    //public class CompressionValueConverter : ValueConverter
    //{
    //    CompressionHelper
    //    public CompressionValueConverter()
    //    {
    //    }
    //    public override object ConvertToStorageType(object value)
    //    {
    //        if (value != null && !(value is byte[]))
    //        {
    //            throw new ArgumentException();
    //        }
    //        if (value == null || ((byte[])value).Length == 0)
    //        {
    //            return value;
    //        }
    //        return CompressionHelper.Compress(new MemoryStream((byte[])value)).ToArray();
    //    }
    //    public override object ConvertFromStorageType(object value)
    //    {
    //        if (value != null && !(value is byte[]))
    //        {
    //            throw new ArgumentException();
    //        }
    //        if (value == null || ((byte[])value).Length == 0)
    //        {
    //            return value;
    //        }
    //        return CompressionHelper.Decompress(new MemoryStream((byte[])value)).ToArray();
    //    }
    //    public override Type StorageType
    //    {
    //        get { return typeof(byte[]); }
    //    }
    //}
}

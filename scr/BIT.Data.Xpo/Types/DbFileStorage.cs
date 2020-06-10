using BIT.Xpo.Converters;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace BIT.Data.Models
{
    public class DbFileStorage : DevExpress.Xpo.XPObject
    {
        public DbFileStorage()
        {

        }

        public DbFileStorage(Session session) : base(session)
        {

        }

        public DbFileStorage(Session session, XPClassInfo classInfo) : base(session, classInfo)
        {

        }

        private string fileName = "";
#if MediumTrust
		private int size;
		public int Size {
			get { return size; }
			set { SetPropertyValue("Size", ref size, value); }
		}
#else
        [Persistent]
        private int size;
        public int Size
        {
            get { return size; }
        }
#endif

        public virtual void LoadFromStream(string fileName, Stream stream)
        {
            Guard.ArgumentNotNull(stream, "stream");
            FileName = fileName;
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            Content = bytes;
        }
        public virtual void SaveToStream(Stream stream)
        {
            if (Content != null)
            {
                stream.Write(Content, 0, Size);
            }
            stream.Flush();
        }
        public void Clear()
        {
            Content = null;
            FileName = String.Empty;
        }
        public override string ToString()
        {
            return FileName;
        }
        [Size(260)]
        public string FileName
        {
            get { return fileName; }
            set { SetPropertyValue("FileName", ref fileName, value); }
        }
        [Persistent, Delayed(true)]
        [ValueConverter(typeof(CompressionValueConverter))]
        [MemberDesignTimeVisibility(false)]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public byte[] Content
        {
            get { return GetDelayedPropertyValue<byte[]>("Content"); }
            set
            {
                int oldSize = size;
                if (value != null)
                {
                    size = value.Length;
                }
                else
                {
                    size = 0;
                }
                SetDelayedPropertyValue("Content", value);
                OnChanged("Size", oldSize, size);
            }
        }
        #region IEmptyCheckable Members
        [NonPersistent, MemberDesignTimeVisibility(false)]
        public bool IsEmpty
        {
            get { return string.IsNullOrEmpty(FileName); }
        }
        #endregion

    }
}

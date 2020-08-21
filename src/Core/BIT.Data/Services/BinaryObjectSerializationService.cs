using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;

namespace BIT.Data.Services
{
    public class BinaryObjectSerializationService : IObjectSerializationService
    {
        StreamType _StreamType;
        public BinaryObjectSerializationService(StreamType StreamType)
        {
            _StreamType = StreamType;
        }
        public T GetObjectsFromByteArray<T>(byte[] bytes)
        {
            var CompressedStream = GetStream(_StreamType, CompressionMode.Decompress, new MemoryStream(bytes));
            MemoryStream memStream = new MemoryStream();

            CompressedStream.CopyTo(memStream);

            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(bytes, 0, bytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            T obj = (T)binForm.Deserialize(memStream);
            return obj;
        }
        Stream GetStream(StreamType StreamType, CompressionMode compressionMode,Stream OrigialStream)
        {
            switch (StreamType)
            {
                case StreamType.GZip:
                    return new GZipStream(OrigialStream,
                           compressionMode);

                case StreamType.Deflated:
                    return new DeflateStream(OrigialStream, compressionMode);

                case StreamType.UnCompressed:
                    return OrigialStream;
                default:
                    return null;
            }
        }
        Stream GetStream(StreamType StreamType, CompressionMode compressionMode)
        {

            return GetStream(StreamType, compressionMode, new MemoryStream());
           
        }
        MemoryStream ExtractMemoryStream(StreamType StreamType,Stream stream)
        {
            switch (StreamType)
            {
                case StreamType.GZip:
                    return ((GZipStream)stream).BaseStream as MemoryStream;
              
                case StreamType.Deflated:
                    return ((DeflateStream)stream).BaseStream as MemoryStream;
                   
                case StreamType.UnCompressed:
                    
                    return stream as MemoryStream;
                default:
                    return null;
            }
         
        }
        public byte[] ToByteArray<T>(T Data)
        {
            if (Data == null)
                return null;

       
            BinaryFormatter bf = new BinaryFormatter();
            var ms = this.GetStream(_StreamType, CompressionMode.Compress);
            bf.Serialize(ms, Data);

            return ExtractMemoryStream(_StreamType,ms).ToArray();
        }
    }
}

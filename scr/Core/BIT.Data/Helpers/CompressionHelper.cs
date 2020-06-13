
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace BIT.Data.Helpers
{
    public class CompressionHelper
    {
        private const int RatesArraySize = 256;
        private const float RangeWidth = 0.1F;
        private static Guid Version2Prefix = new Guid("DA088B12-6641-413b-BBFC-2829752DCF96");
        private const string Version2XafCompressedYesString = "+";
        private const string Version2XafCompressedNoString = "-";
        private const int MinAlwaysCompressedLenght = 1000000;
        private static bool IsGoodCompressionForecast(MemoryStream data)
        {
            if (data != null)
            {
                int[] rates = new int[RatesArraySize];
                int inRange = 0;
                int usedBytes = 0;
                for (int i = 0; i < RatesArraySize; i++)
                {
                    rates[i] = 0;
                }
                while (data.Position != data.Length)
                {
                    rates[data.ReadByte()]++;
                }
                for (int i = 0; i < RatesArraySize; i++)
                {
                    if (rates[i] > 0)
                    {
                        usedBytes++;
                    }
                }
                int lowBoundary = (int)((data.Length / usedBytes) * (1 - RangeWidth / 2));
                int highBoundary = (int)((data.Length / usedBytes) * (1 + RangeWidth / 2));
                for (int i = 0; i < RatesArraySize; i++)
                {
                    if (rates[i] > lowBoundary && rates[i] < highBoundary)
                    {
                        inRange++;
                    }
                }
                if (inRange < (int)(usedBytes * RangeWidth))
                {
                    return true;
                }
            }
            return false;
        }
        private static void WriteHeader(bool isCompressed, MemoryStream result)
        {
            byte[] versionPrefix = Version2Prefix.ToByteArray();
            string headerString = isCompressed ? Version2XafCompressedYesString : Version2XafCompressedNoString;
            byte[] header = System.Text.Encoding.UTF8.GetBytes(headerString.ToCharArray());
            result.Write(versionPrefix, 0, versionPrefix.Length);
            result.Write(header, 0, header.Length);
        }
        private static MemoryStream CreateVersion2CompressedStream(MemoryStream compressed, bool isCompressed)
        {
            MemoryStream result = new MemoryStream();
            WriteHeader(isCompressed, result);
            compressed.WriteTo(result);
            return result;
        }
        private static MemoryStream DecompressData(MemoryStream ms)
        {
            int BufferSize = 5196;
            MemoryStream result = new MemoryStream();
            using (GZipStream inStream = new GZipStream(ms, CompressionMode.Decompress, true))
            {
                byte[] buffer = new byte[BufferSize];
                while (true)
                {
                    int readCount = inStream.Read(buffer, 0, BufferSize);
                    if (readCount == 0)
                    {
                        break;
                    }
                    result.Write(buffer, 0, readCount);
                }
            }
            return result;
        }
        private static MemoryStream DecompressVersion2Stream(MemoryStream ms)
        {
            byte[] header = new byte[System.Text.Encoding.UTF8.GetBytes(Version2XafCompressedYesString.ToCharArray()).Length];
            ms.Read(header, 0, header.Length);
            string headerString = System.Text.Encoding.UTF8.GetString(header, 0, header.Length);
            if (headerString == Version2XafCompressedYesString)
            {
                return DecompressData(ms);
            }
            if (headerString == Version2XafCompressedNoString)
            {
                MemoryStream result = new MemoryStream();
                while (ms.Position < ms.Length)
                {
                    result.WriteByte((byte)ms.ReadByte());
                }
                return result;
            }
            throw new ArgumentException();
        }
        public static MemoryStream Compress(MemoryStream data)
        {
            if (data != null && data.Length > 0)
            {
                if ((data.Length < MinAlwaysCompressedLenght) || IsGoodCompressionForecast(data))
                {
                    using (MemoryStream compressed = new MemoryStream())
                    {
                        using (GZipStream deflater = new GZipStream(compressed, CompressionMode.Compress, true))
                        {
                            data.WriteTo(deflater);
                        }
                        if (compressed.Length < data.Length)
                        {
                            return CreateVersion2CompressedStream(compressed, true);
                        }
                    }
                }
                return CreateVersion2CompressedStream(data, false);
            }
            return data;
        }
        public static MemoryStream Decompress(MemoryStream data)
        {
            if (data != null && data.Length > 0)
            {
                long startPosition = data.Position;
                byte[] guidPrefix = new byte[16];
                data.Read(guidPrefix, 0, guidPrefix.Length);
                if (new Guid(guidPrefix) == Version2Prefix)
                {
                    return DecompressVersion2Stream(data);
                }
                else
                {
                    data.Position = startPosition;
                    return DecompressData(data);
                }
            }
            return data;
        }
   
    }
}

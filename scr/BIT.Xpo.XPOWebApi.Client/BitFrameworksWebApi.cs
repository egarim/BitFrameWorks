
using BIT.Data.Helpers;
using BIT.Data.Models;
using DevExpress.Xpo.DB;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Xml;
using System.Xml.Serialization;

namespace BIT.Xpo.Providers.WebApi.Client
{
    public class BitFrameworksWebApi
    { 

     
        private  string _Url;
        private  string _Token;
        private  string _DataStoreId;
        private string _LoginController;
        private string _WebApiController;

        RestClient client;
       
        public BitFrameworksWebApi(string Url, object AutoCreateOption, string Token,string DataStoreId)

        {

            InternalConstructor(Url, "api/XPOWebApi", "api/Login", AutoCreateOption, Token, DataStoreId);
        }
     
        public BitFrameworksWebApi(string Url,string WebApicontroller,string LoginController, object AutoCreateOption, string Token, string DataStoreId)
        {
            InternalConstructor(Url, WebApicontroller, LoginController, AutoCreateOption, Token, DataStoreId);
         
        }
        void InternalConstructor(string Url,string WebApiController,string LoginController, object AutoCreateOption, string Token, string DataStoreId)
        {
            this._Url = Url;
            this._WebApiController = WebApiController;
            this._LoginController = LoginController;
            this._Token = Token;
            this._AutoCreateOption = AutoCreateOption;
            this._DataStoreId = DataStoreId;
          
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            client = new RestClient(Url);
        }

        public object _AutoCreateOption { get; private set; }

        public T ModifyData<T>(byte[] dmlStatements)
        {
            RestRequest request = PrepareRequest();
            request.Resource = $"{_WebApiController}/ModifyData";
            request.AddParameter("dmlStatements", dmlStatements, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.IsSuccessful)
            {
                var Bytes = JsonConvert.DeserializeObject<Byte[]>(response.Content);
                return GetObjectsFromByteArray<T>(Bytes);
            }
            throw ThrowFormatedException(response);
        }

        private RestRequest PrepareRequest()
        {
            var request = new RestRequest(Method.POST);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/octet-stream");
            request.AddHeader("Token", _Token);
            request.AddHeader("AutoCreateOptions", _AutoCreateOption.ToString());
            request.AddHeader("DataStoreId", _DataStoreId);
            return request;
        }

        private static byte[] Compress(byte[] raw)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                using (GZipStream gzip = new GZipStream(memory,
                    CompressionMode.Compress, true))
                {
                    gzip.Write(raw, 0, raw.Length);
                }

                return memory.ToArray();
            }
        }

        public static T GetObjectsFromByteArray<T>(byte[] bytes)
        {
            var Type = typeof(T);
            using (MemoryStream fs = new MemoryStream(bytes))
            using (var gZipStream = new GZipStream(fs, CompressionMode.Decompress))
            {
                using (XmlDictionaryReader reader =
                    XmlDictionaryReader.CreateTextReader(gZipStream, XmlDictionaryReaderQuotas.Max))
                {
                    XmlSerializer serializer = new XmlSerializer(Type);
                    var Statement = (T)Convert.ChangeType(serializer.Deserialize(reader), Type);
                    return Statement;
                }
            }
        }
        public static string SerializeObject<T>(T toSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
        }
        public static byte[] ToByteArray<T>(T Data)
        {
            try
            {
                var StatementType = typeof(T);

                var fs = new MemoryStream();
                using (XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(fs))
                {
                    XmlSerializer serializer = new XmlSerializer(StatementType);
                    serializer.Serialize(writer, Data);

                }
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    //HACK only for debug how much data are we sending
                    var array = fs.ToArray();
                    Debug.WriteLine(string.Format("{0}:{1} kb", "Length before compression", Convert.ToDecimal(array.Length) / Convert.ToDecimal(1000)));
                    array = Compress(array);
                    Debug.WriteLine(string.Format("{0}:{1} kb", "Length after compression", Convert.ToDecimal(array.Length) / Convert.ToDecimal(1000)));
                    return array;
                }
                return Compress(fs.ToArray());
            }
            catch (Exception exception)
            {
                Debug.WriteLine(string.Format("{0}:{1}", "exception.Message", exception.Message));
                if (exception.InnerException != null)
                {
                    Debug.WriteLine(string.Format("{0}:{1}", "exception.InnerException.Message", exception.InnerException.Message));
                }
                Debug.WriteLine(string.Format("{0}:{1}", " exception.StackTrace", exception.StackTrace));
            }
            return null;
        }

        public T SelectData<T>(byte[] selects)
        {
            RestRequest request = PrepareRequest();
            request.Resource = $"{_WebApiController}/SelectData";
            request.AddParameter("selects", selects, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var Bytes = JsonConvert.DeserializeObject<Byte[]>(response.Content);
                var SelectData = GetObjectsFromByteArray<T>(Bytes);
                return SelectData;
            }
            throw ThrowFormatedException(response);
        }

        private static Exception ThrowFormatedException(IRestResponse response)
        {
            string ExceptioMessage = $"Error status:{response.StatusCode} {System.Environment.NewLine} Error message:{response.StatusDescription} {System.Environment.NewLine} Error message:{response.Content}";
            Debug.WriteLine(ExceptioMessage);
            return new Exception(ExceptioMessage);
        }

        public T UpdateSchema<T>(bool dontCreateIfFirstTableNotExist, string tables) where T : new()
        {
            RestRequest request = PrepareRequest();
            request.Resource = $"{_WebApiController}/UpdateSchema";
     
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add(nameof(dontCreateIfFirstTableNotExist), dontCreateIfFirstTableNotExist);
            Parameters.Add(nameof(tables), tables);
            string output = JsonConvert.SerializeObject(Parameters);
            
           
            byte[] value = ToByteArray(output);
            request.AddParameter("Data", value, ParameterType.RequestBody);
            IRestResponse<T> response = client.Execute<T>(request);
            if (response.IsSuccessful)
            {
                return response.Data;
            }
            throw ThrowFormatedException(response);
        }
        public object Do(string Command, object Args)
        {
           

            Dictionary<string, object> Parameters = new Dictionary<string, object>();
            Parameters.Add(nameof(Command), Command);
            Parameters.Add(nameof(Args), Args);
            string output = JsonConvert.SerializeObject(Parameters);


            RestRequest request = PrepareRequest();
            request.Resource = $"{_WebApiController}/Do";
            request.AddParameter("args", ToByteArray(output), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            if (response.IsSuccessful)
            {
                var Bytes = JsonConvert.DeserializeObject<Byte[]>(response.Content);
                return GetObjectsFromByteArray<object>(Bytes);
            }
            throw ThrowFormatedException(response);
        }
        public LoginResult Login(string UserName, string Password)
        {
            RestRequest request = PrepareRequest();
            request.Resource = $"{_LoginController}/Login";

            LoginParameters Parameters = new LoginParameters();
            Parameters.Username= UserName;
            Parameters.Password =Password;
            string output = JsonConvert.SerializeObject(Parameters);


            byte[] value = ToByteArray(output);
            request.AddParameter("Data", value, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.IsSuccessful)
            {
                //var Bytes = JsonConvert.DeserializeObject<Byte[]>(response.Content);
                //var LoginResult= GetObjectsFromByteArray<LoginResult>(Bytes);


                var LoginResult = JsonConvert.DeserializeObject<LoginResult>(response.Content);
                //var LoginResult = GetObjectsFromByteArray<LoginResult>(Bytes);

                this._Token = LoginResult.Token;
                return LoginResult;
            }
            throw ThrowFormatedException(response);

            //HACK we might need to encrypt username and password
        }


    }
}

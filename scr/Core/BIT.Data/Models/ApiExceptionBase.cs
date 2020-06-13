using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace BIT.Data.Models
{
    public class ApiExceptionBase : Dictionary<string, object>
    {


        public ApiExceptionBase(Exception exception)
        {
            this.ExceptionMessage = exception.Message;
            this.InnerException = exception?.InnerException?.Message;
            this.StackTrace = exception.StackTrace;

        }
        private object _ExceptionMessage;
        [JsonIgnore()]
        public string ExceptionMessage
        {
            get
            {
                this.TryGetValue(nameof(ExceptionMessage), out _ExceptionMessage);

                return _ExceptionMessage?.ToString();
            }
            set
            {
                if (!this.ContainsKey(nameof(ExceptionMessage)))
                    this.Add(nameof(ExceptionMessage), value);
                else
                    this[nameof(ExceptionMessage)] = value;

            }
        }
        private object _StackTrace;
        [JsonIgnore()]
        public string StackTrace
        {
            get
            {
                this.TryGetValue(nameof(StackTrace), out _StackTrace);

                return _StackTrace?.ToString();
            }
            set
            {
                if (!this.ContainsKey(nameof(StackTrace)))
                    this.Add(nameof(StackTrace), value);
                else
                    this[nameof(StackTrace)] = value;

            }
        }
        private object _InnerExceptionMessage;
        [JsonIgnore()]
        public string InnerException
        {
            get
            {
                this.TryGetValue(nameof(InnerException), out _InnerExceptionMessage);

                return _InnerExceptionMessage?.ToString();
            }
            set
            {
                if (!this.ContainsKey(nameof(InnerException)))
                    this.Add(nameof(InnerException), value);
                else
                    this[nameof(InnerException)] = value;

            }
        }
        public ApiExceptionBase()
        {
        }
    }
}
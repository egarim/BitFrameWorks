using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BIT.Data.Models
{

    public class ApiAuthenticationResult : Dictionary<string, object>
    {

        private object _Authenticated;
        [JsonIgnore()]
        public bool Authenticated
        {
            get
            {
                this.TryGetValue(nameof(Authenticated), out _Authenticated);

                return Convert.ToBoolean(_Authenticated?.ToString());
            }
            set
            {
                if (!this.ContainsKey(nameof(Authenticated)))
                    this.Add(nameof(Authenticated), value);
                else
                    this[nameof(Authenticated)] = value;

            }
        }
        public ApiAuthenticationResult(bool authenticated)
        {
            Authenticated = authenticated;

        }
        private object _LastError;
        [JsonIgnore()]
        public string LastError
        {
            get
            {
                this.TryGetValue(nameof(LastError), out _LastError);

                return _LastError?.ToString();
            }
            set
            {
                if (!this.ContainsKey(nameof(LastError)))
                    this.Add(nameof(LastError), value);
                else
                    this[nameof(LastError)] = value;

            }
        }
        private object _UserId;
        [JsonIgnore()]
        public string UserId
        {
            get
            {
                this.TryGetValue(nameof(UserId), out _UserId);

                return _UserId?.ToString();
            }
            set
            {
                if (!this.ContainsKey(nameof(UserId)))
                    this.Add(nameof(UserId), value);
                else
                    this[nameof(UserId)] = value;

            }
        }
        private object _Username;
        [JsonIgnore()]
        public string Username
        {
            get
            {
                this.TryGetValue(nameof(Username), out _Username);

                return _Username?.ToString();
            }
            set
            {
                if (!this.ContainsKey(nameof(Username)))
                    this.Add(nameof(Username), value);
                else
                    this[nameof(Username)] = value;

            }
        }
        public ApiAuthenticationResult()
        {
        }
    }
}
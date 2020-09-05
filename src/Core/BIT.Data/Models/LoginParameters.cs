using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace BIT.Data.Models
{

    public class LoginParameters : Dictionary<string, object>, ILoginParameters
    {

        private object _Username;
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
        private object _Password;
        public string Password
        {
            get
            {
                this.TryGetValue(nameof(Password), out _Password);

                return _Password?.ToString();
            }
            set
            {
                if (!this.ContainsKey(nameof(Password)))
                    this.Add(nameof(Password), value);
                else
                    this[nameof(Password)] = value;

            }
        }
        public LoginParameters(string username, string password)
        {
            Username = username;
            Password = password;
            this.Add(nameof(Username), username);
            this.Add(nameof(Password), password);

        }
        public LoginParameters()
        {


        }


    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace BIT.Data.Models
{
    public class JwtAuthInfo
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public bool AuthenticatioOn { get; set; }
    }
}

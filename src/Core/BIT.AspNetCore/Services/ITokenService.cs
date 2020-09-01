using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace BIT.AspNetCore.Services
{
    public interface ITokenService<out T>
    {
        T GetToken(HttpRequest Context);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClaimBasedAuthorization.Services
{
    public interface IAuthService
    {
        Task<string> Authenticate(string username, string password);
    }
}

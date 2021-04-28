using HealthLogger.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HealthLogger.Services
{

    class AuthenticationService : IAuthenticationService
    {
        public Task<AuthenticationResult> Login(string username, string password)
        {
            var webclient = new WebClient();
            throw new NotImplementedException();
        }

        public Task<AuthenticationResult> Register(string username, string email, string password)
        {
            var webclient = new WebClient();
            throw new NotImplementedException();
        }

        public Task<AuthenticationResult> RegisterAdmin(string username, string email, string password)
        {
            var webclient = new WebClient();
            throw new NotImplementedException();
        }
    }
}

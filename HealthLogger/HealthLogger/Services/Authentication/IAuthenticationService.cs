using HealthLogger.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthLogger.Services
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResult> Login(string username, string password);
        Task<AuthenticationResult> Register(string username, string email, string password);
        Task<AuthenticationResult> RegisterAdmin(string username, string email, string password);

    }
}

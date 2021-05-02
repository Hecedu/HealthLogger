using HealthLogger.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthLogger.Services
{
    public interface IAuthenticationService
    {
        Task<LoginResult> Login(string username, string password);
        Task<RegisterResult> Register(string username, string email, string password);
        Task<RegisterResult> RegisterAdmin(string username, string email, string password);
    }
}

using HealthLogger.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HealthLogger.Services
{

    class AuthenticationService : IAuthenticationService
    {
        public async Task<LoginResult> Login(string username, string password)
        {
            LoginModel login = new LoginModel()
            {
                username = username,
                password = password,
            };
            string json = JsonConvert.SerializeObject(login);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var httpClient = new HttpClient();
            HttpResponseMessage ResponseMessage = await httpClient.PostAsync("https://healthtrackerapi20210423160155.azurewebsites.net/api/Authentication/Login/", content);
            if (ResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<LoginResult>(await ResponseMessage.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception("HTTP Response error. Please check credentials.");
            }
        }

        public async Task<RegisterResult> Register(string username, string email, string password)
        {
            RegisterModel register = new RegisterModel()
            {
                username = username,
                email = email,
                password = password,
            };
            string json = JsonConvert.SerializeObject(register);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var httpClient = new HttpClient();
            HttpResponseMessage ResponseMessage = await httpClient.PostAsync("https://healthtrackerapi20210423160155.azurewebsites.net/api/Authentication/Register/", content);
            if (ResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<RegisterResult>(await ResponseMessage.Content.ReadAsStringAsync());
            }
            return new RegisterResult { status = "Error", message = "Bad HTTP Response" };
        }

        public Task<RegisterResult> RegisterAdmin(string username, string email, string password)
        {

            var webclient = new WebClient();
            throw new NotImplementedException();
        }
    }
}

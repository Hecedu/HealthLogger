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
        public async Task<AuthenticationResult> Login(string username, string password)
        {
            LoginModel login = new LoginModel()
            {
                username = username,
                password = password,
            };
            string json = JsonConvert.SerializeObject(login);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var httpClient = new HttpClient();
            HttpResponseMessage ResponseMessage = await httpClient.PostAsync("https://healthtrackerapi20210423160155.azurewebsites.net/api/Authentication/Login", content);
            if (ResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                string result = await ResponseMessage.Content.ReadAsStringAsync();
                
            }
            return JsonConvert.DeserializeObject<AuthenticationResult>(result);
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

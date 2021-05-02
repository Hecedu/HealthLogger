using HealthLogger.Models;
using Newtonsoft.Json;
using SQLite;
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
        static SQLiteAsyncConnection Database;

        public AuthenticationService()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            Database.CreateTableAsync<MealLog>().Wait();
            Database.CreateTableAsync<ActivityLog>().Wait();
        }
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
            HttpResponseMessage ResponseMessage = await httpClient.PostAsync($"{Settings.HealthTrackerApiUri}/api/Authentication/Login/", content);
            if (ResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                LoginResult result = JsonConvert.DeserializeObject<LoginResult>(await ResponseMessage.Content.ReadAsStringAsync());
                await UpdateMealLogCredentials(result.id);
                await UpdateActivityLogCredentials(result.id);
                return result;
            }
            else
            {
                throw new Exception("HTTP Response error. Please check credentials.");
            }
        }
        public async Task<CloudResult> Register(string username, string email, string password)
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
            HttpResponseMessage ResponseMessage = await httpClient.PostAsync($"{Settings.HealthTrackerApiUri}/api/Authentication/Register/", content);
            if (ResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<CloudResult>(await ResponseMessage.Content.ReadAsStringAsync());
            }
            return new CloudResult { status = "Error", message = "Bad HTTP Response" };
        }
        public Task<CloudResult> RegisterAdmin(string username, string email, string password)
        {

            var webclient = new WebClient();
            throw new NotImplementedException();
        }

        private async Task UpdateMealLogCredentials (string userID)
        {
            foreach (MealLog mealLog in await Database.Table<MealLog>().ToListAsync())
            {
                mealLog.UserId = userID;
                await Database.UpdateAsync(mealLog);
            }
        }
        private async Task UpdateActivityLogCredentials(string userID)
        {
            foreach (ActivityLog activityLog in await Database.Table<ActivityLog>().ToListAsync())
            {
                activityLog.UserId = userID;
                await Database.UpdateAsync(activityLog);
            }
        }
    }
}

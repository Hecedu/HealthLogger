using HealthLogger.Models;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HealthLogger.Services
{
    class CloudStoreService:ICloudStoreService
    {
        HttpClient httpClient = new HttpClient();
        static SQLiteAsyncConnection Database;

        public CloudStoreService()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            Database.CreateTableAsync<MealLog>().Wait();
            Database.CreateTableAsync<ActivityLog>().Wait();
        }

        public async Task Backup()
        {
            try
            {
                await DeleteCloudUserLogs();
                await UploadMealLogs();
                await UploadActivityLogs();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task Sync()
        {
            try
            {
                await DeleteLocalLogs();
                await SaveMealLogsAsync(await DownloadMealLogs());
                await SaveActivityLogsAsync(await DownloadActivityLogs());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteCloudUserLogs()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.JWDToken);
            var url = "https://healthtrackerapi20210423160155.azurewebsites.net/api/HealthTracker/DeleteUserLogs";
            var content = new StringContent(String.Format("userId={0}", Settings.UserId), Encoding.UTF8, "application/json");
            var ResponseMessage = await httpClient.PostAsync(url, content);
            if (ResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"HTTP Response error: {ResponseMessage.StatusCode}. Please check credentials.");
            }
        }
        public async Task DeleteLocalLogs()
        {
            await DeleteLocalActivityLogsAsync();
            await DeleteLocalMealLogsAsync();
        }

        public async Task UploadMealLogs()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.JWDToken);
            var url = "https://healthtrackerapi20210423160155.azurewebsites.net/api/HealthTracker/AddMealLogs";
            var MealLogs = await Database.Table<MealLog>().ToListAsync();
            string json = JsonConvert.SerializeObject(MealLogs);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var ResponseMessage = await httpClient.PostAsync(url, content);
            if (ResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"HTTP Response error: {ResponseMessage.StatusCode}. Please check credentials.");
            }
        }
        public async Task<List<MealLog>> DownloadMealLogs()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.JWDToken);
            var url = "https://healthtrackerapi20210423160155.azurewebsites.net/api/HealthTracker/GetUserMealLogs";
            var content = new StringContent(String.Format("userId={0}", Settings.UserId), Encoding.UTF8, "application/json");
            var ResponseMessage = await httpClient.PostAsync(url, content);
            if (ResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<List<MealLog>>(await ResponseMessage.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception("HTTP Response error. Please check credentials.");
            }
        }
        public async Task SaveMealLogsAsync(List<MealLog> mealLogs)
        {
            foreach (MealLog mealLog in mealLogs)
            {
                if (mealLog.ID != 0)
                {
                    // Update an existing note.
                    await Database.UpdateAsync(mealLog);
                }
                else
                {
                    // Save a new note.
                    await Database.InsertAsync(mealLog);
                }
            }
        }
        public async Task DeleteLocalMealLogsAsync()
        {
            await Database.DeleteAllAsync<MealLog>();
        }

        public async Task UploadActivityLogs()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.JWDToken);
            var url = "https://healthtrackerapi20210423160155.azurewebsites.net/api/HealthTracker/AddActivityLogs";
            var ActivityLogs = await Database.Table<ActivityLog>().ToListAsync();
            string json = JsonConvert.SerializeObject(ActivityLogs);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var ResponseMessage = await httpClient.PostAsync(url, content);
            if (ResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"HTTP Response error: {ResponseMessage.StatusCode}. Please check credentials.");
            }
        }
        public async Task<List<ActivityLog>> DownloadActivityLogs()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.JWDToken);
            var url = "https://healthtrackerapi20210423160155.azurewebsites.net/api/HealthTracker/GetUserActivityLogs";
            var content = new StringContent(String.Format("userId={0}", Settings.UserId), Encoding.UTF8, "application/json");
            var ResponseMessage = await httpClient.PostAsync(url, content);
            if (ResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<List<ActivityLog>>(await ResponseMessage.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception("HTTP Response error. Please check credentials.");
            }
        }
        public async Task SaveActivityLogsAsync(List<ActivityLog> activityLogs)
        {
            foreach (ActivityLog activityLog in activityLogs)
            {
                if (activityLog.ID != 0)
                {
                    // Update an existing note.
                    await Database.UpdateAsync(activityLog);
                }
                else
                {
                    // Save a new note.
                    await Database.InsertAsync(activityLog);
                }
            }
        }
        public async Task DeleteLocalActivityLogsAsync()
        {
            await Database.DeleteAllAsync<ActivityLog>();
        }
    }
}

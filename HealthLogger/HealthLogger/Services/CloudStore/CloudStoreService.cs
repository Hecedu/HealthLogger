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

        private async Task DeleteCloudUserLogs()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.JWDToken);
            var url = $"{Settings.HealthTrackerApiUri}/api/HealthTracker/DeleteUserLogs?userId={Settings.UserId}";

            var ResponseMessage = await httpClient.PostAsync(url, null);
            if (ResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"HTTP Response error: {ResponseMessage.StatusCode}. While uploading meal logs to cloud");
            }
        }
        private async Task DeleteLocalLogs()
        {
            await DeleteLocalActivityLogsAsync();
            await DeleteLocalMealLogsAsync();
        }

        private async Task UploadMealLogs()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.JWDToken);
            var url = $"{Settings.HealthTrackerApiUri}/api/HealthTracker/AddMealLogs";
            List<MealLogPacket> mealLogPackets = new List<MealLogPacket>();
            foreach(MealLog mealLog in await Database.Table<MealLog>().ToListAsync())
            {
                mealLogPackets.Add(new MealLogPacket
                {
                    UserId = mealLog.UserId,
                    Name = mealLog.Name,
                    Calories = mealLog.Calories,
                    Date = mealLog.Date
                });
            }
            string json = JsonConvert.SerializeObject(mealLogPackets);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var ResponseMessage = await httpClient.PostAsync(url, content);
            
            if (ResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"HTTP Response error: {ResponseMessage.StatusCode}. While uploading meal logs to cloud");
            }
        }
        private async Task<List<MealLogPacket>> DownloadMealLogs()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.JWDToken);
            var url = $"{Settings.HealthTrackerApiUri}/api/HealthTracker/GetUserMealLogs?userId={Settings.UserId}";
            var ResponseMessage = await httpClient.PostAsync(url, null);
            if (ResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                List<MealLogPacket> mealLogPackets = new List<MealLogPacket>();
                foreach (MealLog mealLog in JsonConvert.DeserializeObject<List<MealLog>>(await ResponseMessage.Content.ReadAsStringAsync()))
                {
                    mealLogPackets.Add(new MealLogPacket
                    {
                        UserId = mealLog.UserId,
                        Name = mealLog.Name,
                        Calories = mealLog.Calories,
                        Date = mealLog.Date
                    });
                }
                return mealLogPackets;
                
            }
            else
            {
                throw new Exception("HTTP Response error. Please check credentials.");
            }
        }
        private async Task SaveMealLogsAsync(List<MealLogPacket> mealLogPackets)
        {
            foreach (MealLogPacket mealLogPacket in mealLogPackets)
            {
                var mealLog = new MealLog
                {
                    UserId = mealLogPacket.UserId,
                    Name = mealLogPacket.Name,
                    Date = mealLogPacket.Date,
                    Calories = mealLogPacket.Calories
                };
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
        private async Task DeleteLocalMealLogsAsync()
        {
            await Database.DeleteAllAsync<MealLog>();
        }

        private async Task UploadActivityLogs()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.JWDToken);
            var url = $"{Settings.HealthTrackerApiUri}/api/HealthTracker/AddActivityLogs";
            List<ActivityLogPacket> activityLogPackets = new List<ActivityLogPacket>();
            foreach (ActivityLog activityLog in await Database.Table<ActivityLog>().ToListAsync())
            {
                activityLogPackets.Add(new ActivityLogPacket
                {
                    UserId = activityLog.UserId,
                    Name = activityLog.Name,
                    Date = activityLog.Date,
                    CaloriesBurnt = activityLog.CaloriesBurnt,
                    ActiveMinutes = activityLog.ActiveMinutes
                });
            }
            string json = JsonConvert.SerializeObject(activityLogPackets);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var ResponseMessage = await httpClient.PostAsync(url, content);

            if (ResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"HTTP Response error: {ResponseMessage.StatusCode}. While uploading activity logs.");
            }
        }
        private async Task<List<ActivityLogPacket>> DownloadActivityLogs()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.JWDToken);
            var url = $"{Settings.HealthTrackerApiUri}/api/HealthTracker/GetUserActivityLogs?userId={Settings.UserId}";
            var ResponseMessage = await httpClient.PostAsync(url, null);
            if (ResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                List<ActivityLogPacket> activityLogPackets = new List<ActivityLogPacket>();
                foreach (ActivityLog activityLog in JsonConvert.DeserializeObject<List<ActivityLog>>(await ResponseMessage.Content.ReadAsStringAsync()))
                {
                    activityLogPackets.Add(new ActivityLogPacket
                    {
                        UserId = activityLog.UserId,
                        Name = activityLog.Name,
                        CaloriesBurnt = activityLog.CaloriesBurnt,
                        Date = activityLog.Date
                    });
                }
                return activityLogPackets;
            }
            else
            {
                throw new Exception("HTTP Response error. Please check credentials.");
            }
        }
        private async Task SaveActivityLogsAsync(List<ActivityLogPacket> activityLogPackets)
        {
            foreach (ActivityLogPacket activityLogPacket in activityLogPackets)
            {
                var activityLog = new ActivityLog
                {
                    UserId = activityLogPacket.UserId,
                    Name = activityLogPacket.Name,
                    ActiveMinutes = activityLogPacket.ActiveMinutes,
                    Date = activityLogPacket.Date,
                    CaloriesBurnt = activityLogPacket.CaloriesBurnt
                };
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
        private async Task DeleteLocalActivityLogsAsync()
        {
            await Database.DeleteAllAsync<ActivityLog>();
        }
    }
}

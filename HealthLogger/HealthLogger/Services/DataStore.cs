using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthLogger.Models;

namespace HealthLogger.Services
{
    public class DataStore : IDataStore<MealLog,ActivityLog>
    {
        readonly List<MealLog> MealLogs;
        readonly List<ActivityLog> ActivityLogs;

        public DataStore()
        {
            MealLogs = new List<MealLog>()
            {
                new MealLog { Id = Guid.NewGuid().ToString(), Name = "Chicken", Date = DateTime.Now, Calories = 500},
                new MealLog { Id = Guid.NewGuid().ToString(), Name = "Fried oysters", Date = DateTime.Now, Calories = 600},
                new MealLog { Id = Guid.NewGuid().ToString(), Name = "Granola bar", Date = DateTime.Now, Calories = 200},
                new MealLog { Id = Guid.NewGuid().ToString(), Name = "Pasta casserole", Date = DateTime.Now, Calories = 600}
            };
            ActivityLogs = new List<ActivityLog>()
            {
                new ActivityLog {Id = Guid.NewGuid().ToString(), Name = "Run", Date = DateTime.Now, ActiveMinutes = 10, CaloriesBurnt = 100},
                new ActivityLog {Id = Guid.NewGuid().ToString(), Name = "Intense Cardio", Date = DateTime.Now, ActiveMinutes = 20, CaloriesBurnt = 350}
            };
        }

        public async Task<bool> AddMealLogAsync(MealLog mealLog)
        {
            MealLogs.Add(mealLog);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateMealLogAsync(MealLog mealLog)
        {
            var oldMealLog = MealLogs.Where((MealLog arg) => arg.Id == mealLog.Id).FirstOrDefault();
            MealLogs.Remove(oldMealLog);
            MealLogs.Add(mealLog);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteMealLogAsync(string id)
        {
            var oldMealLog = MealLogs.Where((MealLog arg) => arg.Id == id).FirstOrDefault();
            MealLogs.Remove(oldMealLog);

            return await Task.FromResult(true);
        }

        public async Task<MealLog> GetMealLogAsync(string id)
        {
            return await Task.FromResult(MealLogs.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<MealLog>> GetMealLogAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(MealLogs);
        }

        public async Task<bool> AddActivityLogAsync(ActivityLog activityLog)
        {
            ActivityLogs.Add(activityLog);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateActivityLogAsync(ActivityLog activityLog)
        {
            var oldActivityLog = ActivityLogs.Where((ActivityLog arg) => arg.Id == activityLog.Id).FirstOrDefault();
            ActivityLogs.Remove(activityLog);
            ActivityLogs.Add(activityLog);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteActivityLogAsync(string id)
        {
            var oldActivityLog = ActivityLogs.Where((ActivityLog arg) => arg.Id == id).FirstOrDefault();
            ActivityLogs.Remove(oldActivityLog);

            return await Task.FromResult(true);
        }

        public async Task<ActivityLog> GetActivityLogAsync(string id)
        {
            return await Task.FromResult(ActivityLogs.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<ActivityLog>> GetActivityLogAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(ActivityLogs);
        }
    }
}
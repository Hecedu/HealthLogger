using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthLogger.Models;
using SQLite;

namespace HealthLogger.Services
{
    public class DataStore : IDataStore<MealLog,ActivityLog>
    {
        static SQLiteAsyncConnection Database;

        readonly int CalorieGoal;
        readonly int ActiveMinuteGoal;

        public DataStore()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            Database.CreateTableAsync<MealLog>().Wait();
            Database.CreateTableAsync<ActivityLog>().Wait();
            CalorieGoal = 2500;
            ActiveMinuteGoal = 45;
        }

        public async Task<int> SaveMealLogAsync(MealLog mealLog)
        {
            if (mealLog.ID != 0)
            {
                // Update an existing note.
                return await Database.UpdateAsync(mealLog);
            }
            else
            {
                // Save a new note.
                return await Database.InsertAsync(mealLog);
            }
        }
        public async Task<int> SaveMealLogsAsync(List<MealLog> mealLogs)
        {
           int rowsChanged = 0;
           foreach(MealLog mealLog in mealLogs)
            {
                if (mealLog.ID != 0)
                {
                    // Update an existing note.
                    rowsChanged += await Database.UpdateAsync(mealLog);
                }
                else
                {
                    // Save a new note.
                    rowsChanged += await Database.InsertAsync(mealLog);
                }
            }
            return rowsChanged;
        }
        public async Task<int> DeleteMealLogAsync(MealLog mealLog)
        {
            return await Database.DeleteAsync(mealLog);
        }
        public async Task<int> DeleteAllMealLogsAsync(bool forceRefresh = false)
        {
            return await Database.DeleteAllAsync<MealLog>();
        }
        public async Task<MealLog> GetMealLogAsync(int id)
        {
            return await Database.Table<MealLog>()
                .Where(i => i.ID == id)
                .FirstOrDefaultAsync();
        }
        public async Task<List<MealLog>> GetAllMealLogsAsync(bool forceRefresh = false)
        {
            return await Database.Table<MealLog>().ToListAsync();
        }
        
        public async Task<int> SaveActivityLogAsync(ActivityLog activityLog)
        {
            if (activityLog.ID != 0)
            {
                // Update an existing note.
                return await Database.UpdateAsync(activityLog);
            }
            else
            {
                // Save a new note.
                return await Database.InsertAsync(activityLog);
            }
        }
        public async Task<int> SaveActivityLogsAsync(List<ActivityLog> activityLogs)
        {
            int rowsChanged = 0;
            foreach (ActivityLog activityLog in activityLogs)
            {
                if (activityLog.ID != 0)
                {
                    // Update an existing note.
                    rowsChanged += await Database.UpdateAsync(activityLog);
                }
                else
                {
                    // Save a new note.
                    rowsChanged += await Database.InsertAsync(activityLog);
                }
            }
            return rowsChanged;
        }
        public async Task<int> DeleteActivityLogAsync(ActivityLog activityLog)
        {
            return await Database.DeleteAsync(activityLog);
        }
        public async Task<int> DeleteAllActivityLogAsync(bool forceRefresh = false)
        {
            return await Database.DeleteAllAsync<ActivityLog>();
        }
        public async Task<ActivityLog> GetActivityLogAsync(int id)
        {
            return await Database.Table<ActivityLog>()
                 .Where(i => i.ID == id)
                 .FirstOrDefaultAsync();
        }
        public async Task<List<ActivityLog>> GetAllActivityLogsAsync(bool forceRefresh = false)
        {
            return await Database.Table<ActivityLog>().ToListAsync();
        }

        public async Task<int> GetCalorieGoal(bool forceRefresh = false)
        {
            return await Task.FromResult(CalorieGoal);
        }
        public async Task<int> GetActiveMinutesGoal(bool forceRefresh = false)
        {
            return await Task.FromResult(ActiveMinuteGoal);
        }

        public async Task<bool> ChangeLogsUserIdAsync(string id)
        {
            foreach (MealLog mealLog in await GetAllMealLogsAsync())
            {
                mealLog.UserId = id;
                await SaveMealLogAsync(mealLog);
            }
            foreach (ActivityLog activityLog in await GetAllActivityLogsAsync())
            {
                activityLog.UserId = id;
                await SaveActivityLogAsync(activityLog);
            }
            return true;
        }




    }
}
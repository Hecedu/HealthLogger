using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HealthLogger.Models;

namespace HealthLogger.Services
{
    public interface IDataStore<T,U>
    {
        Task<int> SaveMealLogAsync(T mealLog);
        Task<int> SaveMealLogsAsync(List<T> mealLogs);
        Task<int> DeleteMealLogAsync(T mealLog);
        Task<int> DeleteAllMealLogsAsync(bool forceRefresh = false);
        Task<T> GetMealLogAsync(int id);
        Task<List<T>> GetAllMealLogsAsync(bool forceRefresh = false);
        Task<bool> ChangeLogsUserIdAsync(string id);

        Task<int> SaveActivityLogAsync(U activityLog);
        Task<int> SaveActivityLogsAsync(List<U> activityLogs);
        Task<int> DeleteActivityLogAsync(U activityLog);
        Task<int> DeleteAllActivityLogAsync(bool forceRefresh = false);
        Task<U> GetActivityLogAsync(int id);
        Task<List<U>> GetAllActivityLogsAsync(bool forceRefresh = false);

        Task<int> GetCalorieGoal(bool forceRefresh = false);
        Task<int> GetActiveMinutesGoal(bool forceRefresh = false);
    }
}
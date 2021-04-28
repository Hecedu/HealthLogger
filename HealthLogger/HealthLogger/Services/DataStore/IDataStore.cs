using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HealthLogger.Models;

namespace HealthLogger.Services
{
    public interface IDataStore<T,U>
    {
        Task<bool> AddMealLogAsync(T item);
        Task<bool> UpdateMealLogAsync(T item);
        Task<bool> DeleteMealLogAsync(string id);
        Task<T> GetMealLogAsync(string id);
        Task<IEnumerable<T>> GetMealLogAsync(bool forceRefresh = false);
        Task<bool> AddActivityLogAsync(U item);
        Task<bool> UpdateActivityLogAsync(U item);
        Task<bool> DeleteActivityLogAsync(string id);
        Task<U> GetActivityLogAsync(string id);
        Task<IEnumerable<U>> GetActivityLogAsync(bool forceRefresh = false);
        Task<int> GetCalorieGoal(bool forceRefresh = false);
        Task<int> GetActiveMinutesGoal(bool forceRefresh = false);
    }
}
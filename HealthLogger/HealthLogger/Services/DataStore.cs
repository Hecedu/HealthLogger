using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthLogger.Models;

namespace HealthLogger.Services
{
    public class DataStore : IDataStore<MealLog>
    {
        readonly List<MealLog> MealLogs;

        public DataStore()
        {
            MealLogs = new List<MealLog>()
            {
                new MealLog { Id = Guid.NewGuid().ToString(), Name = "Chicken", Date = new DateTime(2021,4,3), Calories = 500},
                new MealLog { Id = Guid.NewGuid().ToString(), Name = "Fried oysters", Date = new DateTime(2021,4,3), Calories = 600}
            };
        }

        public async Task<bool> AddItemAsync(MealLog mealLog)
        {
            MealLogs.Add(mealLog);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(MealLog mealLog)
        {
            var oldItem = MealLogs.Where((MealLog arg) => arg.Id == mealLog.Id).FirstOrDefault();
            MealLogs.Remove(oldItem);
            MealLogs.Add(mealLog);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = MealLogs.Where((MealLog arg) => arg.Id == id).FirstOrDefault();
            MealLogs.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<MealLog> GetItemAsync(string id)
        {
            return await Task.FromResult(MealLogs.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<MealLog>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(MealLogs);
        }
    }
}
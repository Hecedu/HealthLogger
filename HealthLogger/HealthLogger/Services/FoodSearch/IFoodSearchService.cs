using HealthLogger.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthLogger.Services
{
    public interface IFoodSearchService
    {
        Task<FoodResult> GetFood(string searchQuery);
    }
}

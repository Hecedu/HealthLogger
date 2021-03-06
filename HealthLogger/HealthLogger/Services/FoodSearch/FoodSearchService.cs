using HealthLogger.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HealthLogger.Services
{
    class FoodSearchService : IFoodSearchService
    {
        WebClient webClient = new WebClient();

        public async Task<FoodResult> GetFood(string searchQuery)
        {
            string url = GetUrl(searchQuery);
            webClient = new WebClient();
            var json = await webClient.DownloadStringTaskAsync(url);
            return JsonConvert.DeserializeObject<FoodResult>(json);
            
        }
        private string GetUrl(string searchQuery)
        {
            return "https://api.edamam.com/api/food-database/v2/parser?ingr=" +
                                    searchQuery.Replace(" ","%20") +
                                    $"&app_id={Settings.FoodApiId}&app_key={Settings.FoodApiKey}";
        }
    }
}

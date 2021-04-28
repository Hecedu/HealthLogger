using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HealthLogger.Models;
using HealthLogger.Services;
using Xamarin.Forms;

namespace HealthLogger.ViewModels
{
    class SearchMealLogViewModel : BaseViewModel
    {
        public ObservableCollection<Hint> MyFoodList { get; set; }
        FoodResult _foodResult;
        public FoodResult FoodResult
        {
            get => _foodResult;
            set
            {
                _foodResult = value;
                MyFoodList = new ObservableCollection<Hint>(value.Hints);
                OnPropertyChanged();
            }
        }
        
        public override void Init()
        {
            
        }
        public async Task APISearch(string query)
        {
            FoodResult = await FoodService.GetFood(query);
            OnPropertyChanged(nameof(FoodResult));
        }

        public Command<Hint> AddMealLogCommand => new Command<Hint>(async entry => await OnSave(entry));

        public Command<string> PerformSearch => new Command<string>(async (string query) =>
        {
            FoodResult = await FoodService.GetFood(query);
            OnPropertyChanged(nameof(FoodResult));
        });
        
        public SearchMealLogViewModel(INavService navService, IDataStore<MealLog, ActivityLog> dataStore, IFoodSearchService foodService) : base(navService, dataStore, foodService)
        {
            MyFoodList = new ObservableCollection<Hint>();
        }

        private async Task OnSave(Hint entry)
        {
            MealLog newItem = new MealLog()
            {
                Id = Guid.NewGuid().ToString(),
                Calories = Convert.ToInt32(entry.Food.Nutrients.ENERC_KCAL),
                Date = DateTime.Now,
                Name = entry.Food.Label
            };

            await DataStore.AddMealLogAsync(newItem);


            // This will pop the current page off the navigation stack

            await NavService.GoBack();
        }
    }
}

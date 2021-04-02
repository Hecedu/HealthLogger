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
        public ObservableCollection<hint> MyFoodList { get; set; }
        FoodResult _foodResult;
        public FoodResult FoodResult
        {
            get => _foodResult;
            set
            {
                _foodResult = value;
                MyFoodList = new ObservableCollection<hint>(value.hints);
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

        public Command<hint> AddMealLogCommand => new Command<hint>(async entry => await OnSave(entry));

        public Command<string> PerformSearch => new Command<string>(async (string query) =>
        {
            FoodResult = await FoodService.GetFood(query);
            OnPropertyChanged(nameof(FoodResult));
        });
        
        public SearchMealLogViewModel(INavService navService, IDataStore<MealLog, ActivityLog> dataStore, IFoodService foodService) : base(navService, dataStore, foodService)
        {
            MyFoodList = new ObservableCollection<hint>();
        }

        private async Task OnSave(hint entry)
        {
            MealLog newItem = new MealLog()
            {
                Id = Guid.NewGuid().ToString(),
                Calories = Convert.ToInt32(entry.food.nutrients.ENERC_KCAL),
                Date = DateTime.Now,
                Name = entry.food.label
            };

            await DataStore.AddMealLogAsync(newItem);


            // This will pop the current page off the navigation stack

            await NavService.GoBack();
        }
    }
}

using HealthLogger.Models;
using HealthLogger.Views;
using HealthLogger.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HealthLogger.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        public ObservableCollection<MealLog> MealLogs { get; }
        public ObservableCollection<ActivityLog> ActivityLogs { get; }
        public int totalCalories { get; set; }
        public int TotalCalories
        {
            get => totalCalories;
            set
            {
                totalCalories = value;
                OnPropertyChanged();
            }
        }

        public HomePageViewModel(INavService navService, IDataStore<MealLog,ActivityLog> dataStore) : base(navService, dataStore)
        {
            MealLogs = new ObservableCollection<MealLog>();
            ActivityLogs = new ObservableCollection<ActivityLog>();
        }

        public Command<MealLog> ViewMealLogCommand => new Command<MealLog>(async entry => await NavService.NavigateTo<MealLogDetailViewModel, MealLog>(entry));
        public Command<ActivityLog> ViewActivityLogCommand => new Command<ActivityLog>(async entry => await NavService.NavigateTo<ActivityLogDetailViewModel, ActivityLog>(entry));
        //public Command AddMealLogCommand => new Command(async () => await NavService.NavigateTo<NewMealLogViewModel>());
        public Command AddMealLogCommand => new Command(async () => await NavService.NavigateTo<SearchMealLogViewModel>());
        public Command AddActivityLogCommand => new Command(async () => await NavService.NavigateTo<NewActivityLogViewModel>());


        async public override void Init()
        {
            await LoadItems();
            TotalCalories = totalCalories;
        }

        async Task LoadItems()
        {
            IsBusy = true;

            try
            {
                MealLogs.Clear();
                ActivityLogs.Clear();
                totalCalories = 0;
                var mealLogs = await DataStore.GetMealLogAsync(true);
                foreach (var mealLog in mealLogs)
                {
                    totalCalories += mealLog.Calories;
                    MealLogs.Add(mealLog);
                }
                var activityLogs = await DataStore.GetActivityLogAsync(true);
                foreach (var activityLog in activityLogs)
                {
                    totalCalories -= activityLog.CaloriesBurnt;
                    ActivityLogs.Add(activityLog);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
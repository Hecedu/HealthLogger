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
        public ObservableCollection<MealLog> MealLogs { get;  }
        public ObservableCollection<ActivityLog> ActivityLogs { get; }
        
        public int calorieGoal { get; set; }
        public int CalorieGoal { get => calorieGoal;
            set
            {
                calorieGoal = value;
                OnPropertyChanged();
            }
        }
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
        public float caloriesProgress { get; set; }
        public float CaloriesProgress
        {
            get => caloriesProgress;
            set
            {
                if (caloriesProgress <= 1 && caloriesProgress >= 0)
                {
                    caloriesProgress= value;
                    OnPropertyChanged();
                }
                else
                {
                    caloriesProgress = 1;
                }
            }
        }

        public int activeMinutesGoal { get; set; }
        public int ActiveMinutesGoal
        {
            get => activeMinutesGoal;
            set
            {
                activeMinutesGoal = value;
                OnPropertyChanged();
            }
        }

        public int totalActiveMinutes { get; set; }
        public int TotalActiveMinutes
        {
            get => totalActiveMinutes;
            set
            {
                totalActiveMinutes = value;
                OnPropertyChanged();
            }
        }
        public float activeMinutesProgress { get; set; }
        public float ActiveMinutesProgress
        {
            get => activeMinutesProgress;
            set
            {
                if (activeMinutesProgress <= 1 && activeMinutesProgress >= 0)
                {
                    activeMinutesProgress = value;
                    OnPropertyChanged();
                }
                else
                {
                    activeMinutesProgress = 1;
                }
            }
        }

        public HomePageViewModel(INavService navService, IDataStore<MealLog, ActivityLog> dataStore) : base(navService, dataStore)
        {
            MealLogs = new ObservableCollection<MealLog>();
            ActivityLogs = new ObservableCollection<ActivityLog>();
        }

        public Command<MealLog> ViewMealLogCommand => new Command<MealLog>(async entry => await NavService.NavigateTo<MealLogDetailViewModel, MealLog>(entry));
        public Command<ActivityLog> ViewActivityLogCommand => new Command<ActivityLog>(async entry => await NavService.NavigateTo<ActivityLogDetailViewModel, ActivityLog>(entry));
        public Command AuthenticateCommand => new Command(async () => await NavService.NavigateTo<AuthenticationViewModel>());
        public Command AddMealLogCommand => new Command(async () => await NavService.NavigateTo<NewMealLogViewModel>());
        public Command SearchMealLogCommand => new Command(async () => await NavService.NavigateTo<SearchMealLogViewModel>());
        public Command ViewAllMealLogCommand => new Command(async () => await NavService.NavigateTo<ViewAllMealLogViewModel>());
        public Command AddActivityLogCommand => new Command(async () => await NavService.NavigateTo<NewActivityLogViewModel>());
        public Command ViewAllActivityLogCommand => new Command(async () => await NavService.NavigateTo<ViewAllActivityLogViewModel>());

        async public override void Init()
        {
            await LoadItems();
            TotalCalories = totalCalories;
            TotalActiveMinutes = totalActiveMinutes;

            CalorieGoal = await DataStore.GetCalorieGoal(true);
            ActiveMinutesGoal = await DataStore.GetActiveMinutesGoal(true);

            CaloriesProgress = (float)TotalCalories/ (float)CalorieGoal;
            ActiveMinutesProgress = (float)TotalActiveMinutes / (float)ActiveMinutesGoal;

        }

        async Task LoadItems()
        {
            IsBusy = true;

            try
            {
                MealLogs.Clear();
                ActivityLogs.Clear();
                totalCalories = 0;
                totalActiveMinutes = 0;
                var mealLogs = await DataStore.GetAllMealLogsAsync(true);
                foreach (var mealLog in mealLogs)
                {
                    if (mealLog.Date.Date == DateTime.Today.Date)
                    {
                        totalCalories += mealLog.Calories;
                        MealLogs.Add(mealLog);
                    }
                }
                var activityLogs = await DataStore.GetAllActivityLogsAsync(true);
                foreach (var activityLog in activityLogs)
                {
                    if (activityLog.Date.Date == DateTime.Today.Date)
                    {
                        totalCalories -= activityLog.CaloriesBurnt;
                        totalActiveMinutes += activityLog.ActiveMinutes;
                        ActivityLogs.Add(activityLog);
                    }
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
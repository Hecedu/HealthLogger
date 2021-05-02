using HealthLogger.Models;
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
    class ViewAllMealLogViewModel:BaseViewModel
    {
        public ObservableCollection<MealLog> MealLogs { get; }
        public ViewAllMealLogViewModel(INavService navService, IDataStore<MealLog, ActivityLog> dataStore) : base(navService, dataStore)
        {
            MealLogs = new ObservableCollection<MealLog>();
        }
        public Command<MealLog> ViewMealLogCommand => new Command<MealLog>(async entry => await NavService.NavigateTo<MealLogDetailViewModel, MealLog>(entry));
        async public override void Init()
        {
            await LoadItems();
        }
        async Task LoadItems()
        {
            IsBusy = true;

            try
            {
                MealLogs.Clear();
                var mealLogs = await DataStore.GetAllMealLogsAsync(true);
                foreach (var mealLog in mealLogs)
                {
                    MealLogs.Add(mealLog);
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

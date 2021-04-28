using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;
using HealthLogger.Models;
using HealthLogger.Services;

namespace HealthLogger.ViewModels
{
    public class MealLogDetailViewModel : BaseViewModel<MealLog>
    {
        MealLog _mealLog;
        public MealLog MealLog
        {
            get => _mealLog;
            set
            {
                _mealLog = value;
                OnPropertyChanged();
            }
        }


        public MealLogDetailViewModel(INavService navService, IDataStore<MealLog,ActivityLog> dataStore)
         : base(navService, dataStore)
        {
            DeleteCommand = new Command(OnDelete);
        }

        public override void Init(MealLog parameter)
        {
            MealLog = parameter;
        }

        public Command DeleteCommand { get; }

        private async void OnDelete()
        {

            await DataStore.DeleteMealLogAsync(MealLog.Id);
            // This will pop the current page off the navigation stack
            await NavService.GoBack();
        }
    }
}
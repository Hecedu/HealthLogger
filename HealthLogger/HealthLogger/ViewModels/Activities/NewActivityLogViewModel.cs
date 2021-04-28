using HealthLogger.Models;
using HealthLogger.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HealthLogger.ViewModels
{
    class NewActivityLogViewModel : BaseViewModel
    {
        private string name;
        private int activeMinutes;
        private int caloriesBurnt;

        public ActivityLog ActivityLog { get; set; }


        public override void Init()
        {
        }

        public NewActivityLogViewModel(INavService navService, IDataStore<MealLog, ActivityLog> dataStore) : base(navService, dataStore)
        {

            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }




        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(name)&&!String.IsNullOrWhiteSpace(caloriesBurnt.ToString());
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        public int ActiveMinutes
        {
            get => activeMinutes;
            set => SetProperty(ref activeMinutes, value);
        }

        public int CaloriesBurnt
        {
            get => caloriesBurnt;
            set => SetProperty(ref caloriesBurnt, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await NavService.GoBack();
        }


        private async void OnSave()
        {
            ActivityLog newItem = new ActivityLog()
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name,
                Date = DateTime.Now,
                ActiveMinutes = ActiveMinutes,
                CaloriesBurnt = CaloriesBurnt
            };

            await DataStore.AddActivityLogAsync(newItem);

            // This will pop the current page off the navigation stack

            await NavService.GoBack();
        }
    }
}

using HealthLogger.Models;
using HealthLogger.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HealthLogger.ViewModels
{
    public class NewMealLogViewModel : BaseViewModel
    {
        private string name;
        private int calories;

        public MealLog MealLog { get; set; }


        public override void Init()
        {
        }

        public NewMealLogViewModel(INavService navService, IDataStore<MealLog,ActivityLog> dataStore) : base(navService, dataStore)
        {

            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }


        public Command SaveCommand { get; }
        public Command CancelCommand { get; }



        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(name);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public int Calories
        {
            get => calories;
            set => SetProperty(ref calories, value);
        }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await NavService.GoBack();
        }


        private async void OnSave()
        {
            MealLog newItem = new MealLog()
            {
                UserId = Settings.UserId,
                Name = Name,
                Date = DateTime.Now,
                Calories = Calories,
            };

            await DataStore.SaveMealLogAsync(newItem);


            // This will pop the current page off the navigation stack

            await NavService.GoBack();
        }
    }
}
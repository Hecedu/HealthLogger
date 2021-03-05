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
        private string date;
        private int calories;

        public MealLog mealLog { get; set; }


        public override void Init()
        {
        }

        public NewMealLogViewModel(INavService navService, IDataStore<MealLog> dataStore) : base(navService, dataStore)
        {

            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }




        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(name)
                && !String.IsNullOrWhiteSpace(date);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }

        public int Calories
        {
            get => calories;
            set => SetProperty(ref calories, value);
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
            MealLog newItem = new MealLog()
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name,
                Date = DateTime.Now,
                Calories = Calories,
            };

            await DataStore.AddItemAsync(newItem);


            // This will pop the current page off the navigation stack

            await NavService.GoBack();
        }
    }
}
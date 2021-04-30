using HealthLogger.Models;
using HealthLogger.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HealthLogger.ViewModels
{
    class RegisterPageViewModel : BaseViewModel
    {
        private string username;
        private string email;
        private string password;

        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        public RegisterPageViewModel(INavService navService, IDataStore<MealLog, ActivityLog> dataStore, IAuthenticationService authenticationService, IAlertService alertService) 
            : base(navService, dataStore,authenticationService,alertService)
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
            return !String.IsNullOrWhiteSpace(username) && !String.IsNullOrWhiteSpace(password);
        }
        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await NavService.GoBack();
        }


        private async void OnSave()
        {

            var result = await AuthenticationService.Register(Username, Email,Password);

            if (result.status == "Success")
            {
                await NavService.GoBack();
            }
            else
            {
                await AlertService.ShowErrorAsync(result.message, "Error", "ok");
            }

            // This will pop the current page off the navigation stack

            await NavService.GoBack();
        }
    }
}

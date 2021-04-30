using HealthLogger.Models;
using HealthLogger.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace HealthLogger.ViewModels
{
    class LoginPageViewModel : BaseViewModel
    {
        private string username;
        private string password;

        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        public LoginPageViewModel(INavService navService, IDataStore<MealLog, ActivityLog> dataStore, IAuthenticationService authenticationService, IAlertService alertService) 
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
            return !String.IsNullOrWhiteSpace(username)&&!String.IsNullOrWhiteSpace(password);
        }
        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await NavService.GoBack();
        }


        private async void OnSave()
        {
            try
            {
                var result = await AuthenticationService.Login(Username, Password);
                Settings.JWDToken = result.token;
                Settings.UserId = result.id;
                await NavService.GoBack();
            }
            catch (Exception ex)
            {
                await AlertService.ShowErrorAsync(ex.Message, "Error", "ok");
            }
        }
    }
}

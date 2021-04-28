using HealthLogger.Models;
using HealthLogger.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HealthLogger.ViewModels
{
    class AuthenticationPageViewModel : BaseViewModel
    {
        public AuthenticationPageViewModel(INavService navService, IDataStore<MealLog, ActivityLog> dataStore) : base(navService, dataStore)
        {

        }
        public Command LoginCommand => new Command(async () => await NavService.NavigateTo<LoginPageViewModel>());
        public Command RegisterCommand => new Command(async () => await NavService.NavigateTo<RegisterPageViewModel>());
    }
}

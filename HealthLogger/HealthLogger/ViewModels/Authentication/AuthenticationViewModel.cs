using HealthLogger.Models;
using HealthLogger.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HealthLogger.ViewModels
{
    class AuthenticationViewModel : BaseViewModel
    {
        public AuthenticationViewModel(INavService navService, IDataStore<MealLog, ActivityLog> dataStore) : base(navService, dataStore)
        {

        }
        public Command LoginCommand => new Command(async () => await NavService.NavigateTo<LoginViewModel>());
        public Command RegisterCommand => new Command(async () => await NavService.NavigateTo<RegisterViewModel>());
    }
}

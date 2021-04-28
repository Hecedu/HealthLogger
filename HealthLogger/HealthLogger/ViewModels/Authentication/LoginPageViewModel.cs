using HealthLogger.Models;
using HealthLogger.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HealthLogger.ViewModels
{
    class LoginPageViewModel : BaseViewModel
    {
        public LoginPageViewModel(INavService navService, IDataStore<MealLog, ActivityLog> dataStore) : base(navService, dataStore)
        {
        }
    }
}

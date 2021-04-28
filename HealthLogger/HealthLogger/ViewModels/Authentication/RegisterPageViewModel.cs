using HealthLogger.Models;
using HealthLogger.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthLogger.ViewModels
{
    class RegisterPageViewModel : BaseViewModel
    {
        public RegisterPageViewModel(INavService navService, IDataStore<MealLog, ActivityLog> dataStore) : base(navService, dataStore)
        {
        }
    }
}

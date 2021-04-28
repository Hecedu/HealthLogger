using HealthLogger.Models;
using HealthLogger.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HealthLogger.ViewModels
{
    class ActivityLogDetailViewModel : BaseViewModel<ActivityLog>
    {
        ActivityLog _activityLog;
        public ActivityLog ActivityLog
        {
            get => _activityLog;
            set
            {
                _activityLog = value;
                OnPropertyChanged();
            }
        }


        public ActivityLogDetailViewModel(INavService navService, IDataStore<MealLog, ActivityLog> dataStore)
         : base(navService, dataStore)
        {
            DeleteCommand = new Command(OnDelete);
        }

        public override void Init(ActivityLog parameter)
        {
            ActivityLog = parameter;
        }

        public Command DeleteCommand { get; }

        private async void OnDelete()
        {

            await DataStore.DeleteActivityLogAsync(ActivityLog.Id);
            // This will pop the current page off the navigation stack
            await NavService.GoBack();
        }

    }
}

using HealthLogger.Models;
using HealthLogger.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace HealthLogger.ViewModels 
{
    class ViewAllActivityLogViewModel: BaseViewModel
    {
        public ObservableCollection<ActivityLog> ActivityLogs { get; }
        public ViewAllActivityLogViewModel(INavService navService, IDataStore<MealLog, ActivityLog> dataStore) : base(navService, dataStore)
        {
            ActivityLogs = new ObservableCollection<ActivityLog>();
        }
        async public override void Init()
        {
            await LoadItems();
        }
        async Task LoadItems()
        {
            IsBusy = true;

            try
            {
                ActivityLogs.Clear();
                var activityLogs = await DataStore.GetActivityLogAsync(true);
                foreach (var activityLog in activityLogs)
                {
                    ActivityLogs.Add(activityLog);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}

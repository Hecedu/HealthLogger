using HealthLogger.Models;
using HealthLogger.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HealthLogger.ViewModels
{
    class CloudStorageViewModel : BaseViewModel
    {
        public CloudStorageViewModel(INavService navService, IDataStore<MealLog, ActivityLog> dataStore, ICloudStoreService cloudStoreService, IAlertService alertService)
           : base(navService, dataStore, cloudStoreService, alertService)
        {
            BackupCommand = new Command(OnBackup);
            SyncCommand = new Command(OnSync);
        }

        public Command BackupCommand { get; }
        public Command SyncCommand { get; }

        private async void OnBackup()
        {
            try
            {
                await CloudStoreService.Backup();
            }
            catch (Exception ex)
            {
                await AlertService.ShowErrorAsync(ex.Message, "Error", "ok");
            }
        }
        private async void OnSync()
        {
            try
            {
                await CloudStoreService.Sync();
            }
            catch (Exception ex)
            {
                await AlertService.ShowErrorAsync(ex.Message, "Error", "ok");
            }
        }

    }
}

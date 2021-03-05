using HealthLogger.Models;
using HealthLogger.Views;
using HealthLogger.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HealthLogger.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        private MealLog _selectedItem;

        public ObservableCollection<MealLog> MealLogs { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<MealLog> ItemTapped { get; }

        public HomePageViewModel(INavService navService, IDataStore<MealLog> dataStore) : base(navService, dataStore)
        {
            MealLogs = new ObservableCollection<MealLog>();
        }

        public Command<MealLog> ViewCommand => new Command<MealLog>(async entry => await NavService.NavigateTo<HomePageViewModel, MealLog>(entry));
        public Command AddCommand => new Command(async () => await NavService.NavigateTo<NewMealLogViewModel>());
        async public override void Init()
        {
            await LoadItems();
        }
        async Task LoadItems()
        {
            IsBusy = true;

            try
            {
                MealLogs.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    MealLogs.Add(item);
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
        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewMealLogPage));
        }

    }
}
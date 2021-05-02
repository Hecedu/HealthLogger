using HealthLogger.Models;
using HealthLogger.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace HealthLogger.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {

        bool isBusy = false;
        protected INavService NavService { get; private set; }
        protected IDataStore<MealLog, ActivityLog> DataStore { get; private set; }
        protected IFoodSearchService FoodService { get; private set; }
        protected IAuthenticationService AuthenticationService { get; private set; }
        protected IAlertService AlertService { get; private set; }

        protected BaseViewModel(INavService navService, IDataStore<MealLog, ActivityLog> dataStore)
        {
            NavService = navService;
            DataStore = dataStore;
        }
        protected BaseViewModel(INavService navService, IDataStore<MealLog, ActivityLog> dataStore, IFoodSearchService foodService)
        {
            NavService = navService;
            DataStore = dataStore;
            FoodService = foodService;
        }
        protected BaseViewModel(INavService navService, IDataStore<MealLog, ActivityLog> dataStore, IAuthenticationService authenticationService)
        {
            NavService = navService;
            DataStore = dataStore;
            AuthenticationService = authenticationService;
        }
        protected BaseViewModel(INavService navService, IDataStore<MealLog, ActivityLog> dataStore, IAuthenticationService authenticationService, IAlertService alertService)
        {
            NavService = navService;
            DataStore = dataStore;
            AuthenticationService = authenticationService;
            AlertService = alertService;
        }
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }


        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public virtual void Init()
        {
        }
    }
    //
    public class BaseViewModel<TParameter> : BaseViewModel
    {
        protected BaseViewModel(INavService navService, IDataStore<MealLog, ActivityLog> dataStore)
            : base(navService, dataStore)
        {
        }
        protected BaseViewModel(INavService navService, IDataStore<MealLog, ActivityLog> dataStore, IFoodSearchService foodService)
            : base(navService, dataStore, foodService)
        {
        }
        protected BaseViewModel(INavService navService, IDataStore<MealLog, ActivityLog> dataStore, IAuthenticationService authenticationService)
            : base(navService, dataStore, authenticationService)
        {
        }
        protected BaseViewModel(INavService navService, IDataStore<MealLog, ActivityLog> dataStore, IAuthenticationService authenticationService, IAlertService alertService)
            : base(navService, dataStore, authenticationService, alertService)
        {
        }

        public override void Init()
        {
            Init(default);
        }

        public virtual void Init(TParameter parameter)
        {
        }
    }
}
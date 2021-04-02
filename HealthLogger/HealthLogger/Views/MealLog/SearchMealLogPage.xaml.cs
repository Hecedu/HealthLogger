using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthLogger.Models;
using HealthLogger.Services;
using HealthLogger.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections;
using System.Collections.ObjectModel;

namespace HealthLogger.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchMealLogPage : ContentPage
    {
        SearchMealLogViewModel ViewModel => BindingContext as SearchMealLogViewModel;
        public SearchMealLogPage()
        {
            InitializeComponent();
        }
        async void TextChangedHandler(object sender, TextChangedEventArgs e)
        {
            FoodListView.BeginRefresh();

            await ViewModel.APISearch(e.NewTextValue);
            FoodListView.ItemsSource = ViewModel.MyFoodList.Take(5);
            FoodListView.EndRefresh();
        }
    }
}
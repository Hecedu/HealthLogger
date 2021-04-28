using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HealthLogger.ViewModels;
using HealthLogger.Services;
using System.Diagnostics;

namespace HealthLogger.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MealLogDetailPage : ContentPage
    {
        MealLogDetailViewModel ViewModel => BindingContext as MealLogDetailViewModel;
        public MealLogDetailPage()
        {
            InitializeComponent();

        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            await DeleteButton.RelRotateTo(360, 500);
            await DisplayAlert("Warning", "Activity Log succesfully deleted!", "OK");

        }

    }
}
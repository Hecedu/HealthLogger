using HealthLogger.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HealthLogger.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivityLogDetailPage : ContentPage
    {
        ActivityLogDetailViewModel ViewModel => BindingContext as ActivityLogDetailViewModel;
        public ActivityLogDetailPage()
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
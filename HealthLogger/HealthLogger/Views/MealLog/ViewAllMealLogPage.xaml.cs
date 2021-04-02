using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HealthLogger.ViewModels;

namespace HealthLogger.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewAllMealLogPage : ContentPage
    {
        ViewAllMealLogViewModel ViewModel => BindingContext as ViewAllMealLogViewModel;
        public ViewAllMealLogPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel?.Init();
        }
    }
}
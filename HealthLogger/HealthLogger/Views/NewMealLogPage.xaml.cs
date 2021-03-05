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
    public partial class NewMealLogPage : ContentPage
    {
        NewMealLogViewModel ViewModel => BindingContext as NewMealLogViewModel;
        public NewMealLogPage()
        {
            InitializeComponent();
        }
    }
}
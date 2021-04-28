using HealthLogger.Models;
using HealthLogger.ViewModels;
using HealthLogger.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HealthLogger.Views
{

    public partial class NewMealLogPage : ContentPage
    {
        NewMealLogViewModel ViewModel => BindingContext as NewMealLogViewModel;
        public MealLog MealLog { get; set; }
        
        public NewMealLogPage()
        {
            InitializeComponent();
        }
    }
}
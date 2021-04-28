using HealthLogger.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HealthLogger.Models;
namespace HealthLogger.Views
{
    public partial class NewActivityLogPage : ContentPage
    {

        NewActivityLogViewModel ViewModel => BindingContext as NewActivityLogViewModel;
        public ActivityLog ActivityLog { get; set; }

        public NewActivityLogPage()
        {
            InitializeComponent();
        }
    }
}
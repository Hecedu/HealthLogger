using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using HealthLogger.Views;

namespace HealthLogger
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            //update to match views Hector
            InitializeComponent();
            Routing.RegisterRoute(nameof(MealLogDetailPage), typeof(MealLogDetailPage));
            Routing.RegisterRoute(nameof(NewMealLogPage), typeof(NewMealLogPage));
            Routing.RegisterRoute(nameof(SearchMealLogPage), typeof(SearchMealLogPage));
            Routing.RegisterRoute(nameof(ViewAllMealLogPage), typeof(ViewAllMealLogPage));
            Routing.RegisterRoute(nameof(ActivityLogDetailPage), typeof(ActivityLogDetailPage));
            Routing.RegisterRoute(nameof(NewActivityLogPage), typeof(NewActivityLogPage));
            Routing.RegisterRoute(nameof(ViewAllActivityLogPage), typeof(ViewAllActivityLogPage));
        }
    }
}
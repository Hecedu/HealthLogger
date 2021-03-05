using System;
using System.Collections.Generic;
using System.Text;
using HealthLogger.Services;
using HealthLogger.ViewModels;
using HealthLogger.Views;

namespace HealthLogger.Modules
{
    class HealthLoggerNavModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            var navService = new XamarinFormsNavService();

            navService.RegisterViewMapping(typeof(HomePageViewModel), typeof(HomePage));
            navService.RegisterViewMapping(typeof(MealLogDetailViewModel), typeof(MealLogDetailPage));
            navService.RegisterViewMapping(typeof(NewMealLogViewModel), typeof(NewMealLogPage));

            Bind<INavService>()
            .ToMethod(x => navService)
            .InSingletonScope();
        }
    }
}
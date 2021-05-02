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
            navService.RegisterViewMapping(typeof(SearchMealLogViewModel), typeof(SearchMealLogPage));
            navService.RegisterViewMapping(typeof(ViewAllMealLogViewModel), typeof(ViewAllMealLogPage));
            navService.RegisterViewMapping(typeof(ActivityLogDetailViewModel), typeof(ActivityLogDetailPage));
            navService.RegisterViewMapping(typeof(NewActivityLogViewModel), typeof(NewActivityLogPage));
            navService.RegisterViewMapping(typeof(ViewAllActivityLogViewModel), typeof(ViewAllActivityLogPage));
            navService.RegisterViewMapping(typeof(AuthenticationViewModel), typeof(AuthenticationPage));
            navService.RegisterViewMapping(typeof(LoginViewModel), typeof(LoginPage));
            navService.RegisterViewMapping(typeof(RegisterViewModel), typeof(RegisterPage));
            navService.RegisterViewMapping(typeof(CloudStorageViewModel), typeof(CloudStoragePage));

            Bind<INavService>()
            .ToMethod(x => navService)
            .InSingletonScope();
        }
    }
}
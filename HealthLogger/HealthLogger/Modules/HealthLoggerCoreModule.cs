using System;
using System.Collections.Generic;
using System.Text;
using HealthLogger.ViewModels;
using HealthLogger.Models;
using HealthLogger.Services;

namespace HealthLogger.Modules
{
    class HealthLoggerCoreModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            // ViewModels [Make sure this matches with current views  Hector]
            Bind<MealLogDetailViewModel>().ToSelf();
            Bind<ActivityLogDetailViewModel>().ToSelf();
            Bind<HomePageViewModel>().ToSelf();
            Bind<IDataStore<MealLog,ActivityLog>>().To<DataStore>().InSingletonScope();
        }
    }
}
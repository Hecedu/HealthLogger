using HealthLogger.Services;
using HealthLogger.Views;
using HealthLogger.ViewModels;

using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HealthLogger.Modules;
using Ninject.Modules;
using Ninject;

namespace HealthLogger
{
    public partial class App : Application
    {
        public IKernel Kernel { get; set; }
        public static bool IsActive { get; internal set; }

        public App(params INinjectModule[] platformModules)
        {
            InitializeComponent();
            //register core services
            Kernel = new StandardKernel(
            new HealthLoggerCoreModule(),
            new HealthLoggerNavModule());
            //register platform services
            Kernel.Load(platformModules);

            SetMainPage();
        }

        void SetMainPage()
        {
            var mainPage = new NavigationPage(new HomePage())
            {
                BindingContext = Kernel.Get<HomePageViewModel>()
            };

            var navService = Kernel.Get<INavService>() as XamarinFormsNavService;

            navService.XamarinFormsNav = mainPage.Navigation;

            MainPage = mainPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
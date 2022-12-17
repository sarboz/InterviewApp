using InterviewApp.Core.Abstractions;
using InterviewApp.Core.Services;
using InterviewApp.Core.ViewModels;
using InterviewApp.Data;
using InterviewApp.Data.Repositories;
using Xamarin.Forms;

namespace InterviewApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var dbContext = new Context(new DatabasePathProvider());
            var dailyRateRepository = new CurrencyRepository(dbContext);

            DependencyResolver.Instance.Register(new CurrencyApi());
            DependencyResolver.Instance.Register(dbContext);
            DependencyResolver.Instance.Register(dailyRateRepository);
            DependencyResolver.Instance.Register(new CurrencyService());
            DependencyResolver.Instance.Register(new MainViewModel());
            DependencyResolver.Instance.Register(new SettingsViewModel());
            INavigationService navigationService = new NavigationService();
            DependencyResolver.Instance.Register(navigationService);


            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
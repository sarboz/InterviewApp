using System;
using InterviewApp.Core.Services;
using InterviewApp.Core.ViewModels;
using InterviewApp.Data;
using InterviewApp.Data.Abstractions;
using InterviewApp.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace InterviewApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            IApplicationContext dbContext = new Context(new DatabasePathProvider());
            var dailyRateRepository = new DailyRateRepository(dbContext);
            DependencyResolver.Instance.Register(new CurrencyApi());
            DependencyResolver.Instance.Register(dbContext);
            DependencyResolver.Instance.Register(dailyRateRepository);
            DependencyResolver.Instance.Register(new CurrencyService());
            DependencyResolver.Instance.Register(new MainViewModel());


            MainPage = new MainPage();
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
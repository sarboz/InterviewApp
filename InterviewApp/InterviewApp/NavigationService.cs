using System.Collections.Generic;
using InterviewApp.Core.Abstractions;
using Xamarin.Forms;

namespace InterviewApp;

public class NavigationService : INavigationService
{
    private static readonly Dictionary<string, ContentPage> _dictionary= new();

    public NavigationService()
    {
        _dictionary.Add("MainPage", new MainPage());
        _dictionary.Add("Settings", new SettingsPage());
    }

    public void NavigateTo(string pageKey)
    {
        Application.Current.MainPage.Navigation.PushAsync(_dictionary[pageKey]);
    }
}
using InterviewApp.Core.ViewModels;

namespace InterviewApp.Core.Services;

public class ViewModelLocator
{
    public  MainViewModel MainViewModel => DependencyResolver.Instance.Resolve<MainViewModel>();
    public  SettingsViewModel SettingsViewModel => DependencyResolver.Instance.Resolve<SettingsViewModel>();
}
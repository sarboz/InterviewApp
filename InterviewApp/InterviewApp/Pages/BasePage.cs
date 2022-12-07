using InterviewApp.Core.Services;
using InterviewApp.Core.ViewModels;
using Xamarin.Forms;

namespace InterviewApp
{
    public class BasePage<TViewModel> : ContentPage where TViewModel : BaseViewModel
    {
        protected override void OnAppearing()
        {
            var viewModel = DependencyResolver.Instance.Resolve<TViewModel>();
            viewModel.OnAppearing();
        }
    }
}
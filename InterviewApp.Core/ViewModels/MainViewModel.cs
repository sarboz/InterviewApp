using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using InterviewApp.Core.Abstractions;
using InterviewApp.Core.Models;
using InterviewApp.Core.Services;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;

namespace InterviewApp.Core.ViewModels;

public class MainViewModel : BaseViewModel
{
    private CurrencyService _currencyService;
    public ObservableCollection<CurrencyModel> Currencies { get; set; } = new();

    public MainViewModel()
    {
        _currencyService = DependencyResolver.Instance.Resolve<CurrencyService>();

        NavigateCommand = new Command(Navigate);
    }

    private void Navigate()
    {
        var navigationService = DependencyResolver.Instance.Resolve<INavigationService>();
        navigationService.NavigateTo("Settings");
    }

    public Command NavigateCommand { get; }

    public override async Task OnAppearing()
    {
        Currencies.Clear();
        var currencyByDate = await _currencyService.GetCurrenciesModel();
        currencyByDate.ForEach(Currencies.Add);
    }
}
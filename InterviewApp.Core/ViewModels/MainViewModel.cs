using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using InterviewApp.Core.Services;
using InterviewApp.Data.Dtos;
using InterviewApp.Data.Entities;

namespace InterviewApp.Core.ViewModels;

public class MainViewModel : BaseViewModel
{
    private CurrencyService _currencyService;
    public ObservableCollection<Currency> Currencies { get; set; } = new();

    public MainViewModel()
    {
        _currencyService = DependencyResolver.Instance.Resolve<CurrencyService>();
    }

    public override async Task OnAppearing()
    {
        var currencyByDate = await _currencyService.GetCurrencyByDate(DateTime.Now);
        if (currencyByDate.Any())
        {
            currencyByDate.ForEach(Currencies.Add);
            return;
        }

        var remoteCurrencies = await _currencyService.GetRemoteCurrencies(DateTime.Now);
        remoteCurrencies.ForEach(Currencies.Add);
    }
}
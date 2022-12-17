using System.Collections.ObjectModel;
using System.Threading.Tasks;
using InterviewApp.Core.Services;
using InterviewApp.Data.Dtos;
using InterviewApp.Data.Entities;
using Xamarin.Forms;

namespace InterviewApp.Core.ViewModels;

public class SettingsViewModel : BaseViewModel
{
    private CurrencyService _currencyService;
    public ObservableCollection<SettingsEntity> CurrenciesSettings { get; set; } = new();

    public Command<SettingsEntity> SelectedChangedCommand { get; }

    public SettingsViewModel()
    {
        _currencyService = DependencyResolver.Instance.Resolve<CurrencyService>();
        SelectedChangedCommand = new Command<SettingsEntity>(async currency => await SelectedChanged(currency));
    }

    private Task SelectedChanged(SettingsEntity settingsEntity)
    {
        return _currencyService.UpdateCurrencySettings(settingsEntity);
    }

    public override async Task OnAppearing()
    {
        CurrenciesSettings.Clear();
        var currencyByDate = await _currencyService.GetCurrencyTypes();
        currencyByDate.ForEach(CurrenciesSettings.Add);
    }
}
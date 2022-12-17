using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterviewApp.Core.Mappers;
using InterviewApp.Core.Models;
using InterviewApp.Data.Dtos;
using InterviewApp.Data.Entities;
using InterviewApp.Data.Repositories;

namespace InterviewApp.Core.Services;

public class CurrencyService
{
    private CurrencyRepository _currencyRepository;
    private CurrencyApi _currencyApi;

    public CurrencyService()
    {
        _currencyRepository = DependencyResolver.Instance.Resolve<CurrencyRepository>();
        _currencyApi = DependencyResolver.Instance.Resolve<CurrencyApi>();
    }

    public async Task<List<CurrencyModel>> GetCurrenciesModel()
    {
        var currentDay = await GetCurrencyByDate(DateTime.Today);
        var yesterdayOrTomorrow = await GetCurrencyByDate(DateTime.Today.AddDays(1));
        if (!yesterdayOrTomorrow.Any())
            yesterdayOrTomorrow = await GetCurrencyByDate(DateTime.Today.AddDays(-1));
        var mapCurrencyToCurrencyModel = currentDay.MapCurrencyToCurrencyModel();

        foreach (var currency in yesterdayOrTomorrow)
        {
            var isExists = mapCurrencyToCurrencyModel.TryGetValue(currency.CharCode, out var currencyModel);
            if (isExists)
                currencyModel!.OlderRate = currency.Rate;
        }

        return mapCurrencyToCurrencyModel.Values.ToList();
    }

    public async Task<List<Currency>> GetCurrencyByDate(DateTime dateTime)
    {
        var currencies = await _currencyRepository.GetByDate(dateTime);
        if (currencies.Any())
            return currencies;
        var remoteCurrencies = await GetRemoteCurrencies(dateTime);
        await StoreRates(remoteCurrencies, dateTime);
        return await _currencyRepository.GetByDate(dateTime);
    }

    public async Task<List<SettingsEntity>> GetCurrencyTypes()
    {
        var currencies = await _currencyRepository.GetAllCurrencyTypes();
        return currencies;
    }

    private async Task<List<Currency>> GetRemoteCurrencies(DateTime dateTime)
    {
        var currencyDtos = await _currencyApi.GetByDate(dateTime);
        var mapCurrencyDtosToCurrencyEntity = currencyDtos.MapCurrencyDtosToCurrencyEntity();
        return mapCurrencyDtosToCurrencyEntity;
    }

    private async Task StoreRates(List<Currency> currencies, DateTime dateTime)
    {
        var dailyRate = new DailyRate
        {
            Date = dateTime,
            Currency = currencies
        };
        _currencyRepository.Add(dailyRate);
        await _currencyRepository.InitCurrencySettings(currencies);
        await _currencyRepository.SaveChanges();
    }

    public Task UpdateCurrencySettings(SettingsEntity settingsEntity)
    {
        return _currencyRepository.Update(settingsEntity);
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InterviewApp.Core.Mappers;
using InterviewApp.Data.Entities;
using InterviewApp.Data.Repositories;

namespace InterviewApp.Core.Services;

public class CurrencyService
{
    private DailyRateRepository _dailyRateRepository;
    private CurrencyApi _currencyApi;

    public CurrencyService()
    {
        _dailyRateRepository = DependencyResolver.Instance.Resolve<DailyRateRepository>();
        _currencyApi = DependencyResolver.Instance.Resolve<CurrencyApi>();
    }

    public async Task<List<Currency>> GetCurrencyByDate(DateTime dateTime)
    {
        return (await _dailyRateRepository.GetByDate(dateTime))?.Currency ?? new List<Currency>();
    }

    public async Task<List<Currency>> GetRemoteCurrencies(DateTime dateTime)
    {
        var currencyDtos = await _currencyApi.GetByDate(dateTime);
        var mapCurrencyDtosToCurrencyEntity = currencyDtos.MapCurrencyDtosToCurrencyEntity();
        _dailyRateRepository.Add(new DailyRate() { Currency = mapCurrencyDtosToCurrencyEntity, Date = dateTime });
        await _dailyRateRepository.SaveChanges();
        return mapCurrencyDtosToCurrencyEntity;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterviewApp.Data.Dtos;
using InterviewApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace InterviewApp.Data.Repositories;

public class CurrencyRepository
{
    private readonly Context _applicationContext;

    public CurrencyRepository(Context applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public async Task<List<Currency>> GetByDate(DateTime dateTime)
    {
        var disabledItems =
            await _applicationContext.SettingsEntities.Where(entity => entity.IsHide == true).ToListAsync() ??
            new List<SettingsEntity>();

        var items = await _applicationContext.Currencies.Include(currency => currency.DailyRate)
            .Where(currency => currency.DailyRate.Date.Date.Equals(dateTime.Date))
            .ToListAsync();
        return items.Where(currency => disabledItems.All(entity => entity.CurrencyCharCode != currency.CharCode))
            .ToList();
    }

    public async Task<List<SettingsEntity>> GetAllCurrencyTypes()
    {
        return await _applicationContext.SettingsEntities.ToListAsync();
    }

    public void Add(DailyRate rate)
    {
        _applicationContext.Add(rate);
    }

    public Task SaveChanges()
    {
        return _applicationContext.SaveChangesAsync();
    }

    public Task Update(SettingsEntity settingsEntity)
    {
        _applicationContext.Update(settingsEntity);
        return _applicationContext.SaveChangesAsync();
    }

    public async Task InitCurrencySettings(List<Currency> currencies)
    {
        var settings = await _applicationContext.SettingsEntities.ToListAsync();
        foreach (var currency in currencies)
        {
            if (settings.Any(entity => entity.CurrencyCharCode == currency.CharCode))
                continue;
            _applicationContext.SettingsEntities.Add(new SettingsEntity
                { CurrencyCharCode = currency.CharCode, IsHide = false });
        }

        await SaveChanges();
    }
}
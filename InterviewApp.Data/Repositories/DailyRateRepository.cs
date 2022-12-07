using System;
using System.Linq;
using System.Threading.Tasks;
using InterviewApp.Data.Abstractions;
using InterviewApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace InterviewApp.Data.Repositories;

public class DailyRateRepository
{
    private readonly IApplicationContext _applicationContext;

    public DailyRateRepository(IApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public Task<DailyRate> GetByDate(DateTime dateTime)
    {
        return _applicationContext.DailyRates.Include(rate => rate.Currency)
            .FirstOrDefaultAsync(rate => rate.Date.Date.Equals(dateTime.Date));
    }

    public void Add(DailyRate rate)
    {
        _applicationContext.Add(rate);
    }

    public Task SaveChanges()
    {
        return _applicationContext.SaveChangesAsync();
    }
}
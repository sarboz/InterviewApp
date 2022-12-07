using InterviewApp.Data.Abstractions;

namespace InterviewApp.Data.Repositories;

public class CurrencyRepository
{
    private readonly IApplicationContext _applicationContext;

    public CurrencyRepository(IApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }
    
}
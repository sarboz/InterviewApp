using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterviewApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace InterviewApp.Data.Abstractions;

public interface IApplicationContext
{
    void Add(object entity);
    void Update(object entity);
    void Remove(object entity);
    Task<int> SaveChangesAsync();
    public DbSet<Currency> Currencies { get; }
    public DbSet<DailyRate> DailyRates { get; }
    Task<List<T>> GetAll<T>() where T : BaseEntity;
}
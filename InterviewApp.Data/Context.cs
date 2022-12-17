using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DamatMobile.Core.Abstractions;
using InterviewApp.Data.Dtos;
using InterviewApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace InterviewApp.Data;

public class Context : DbContext
{
    private readonly IDatabasePathProvider _pathProvider;

    public Context(IDatabasePathProvider pathProvider)
    {
        _pathProvider = pathProvider;
        base.Database.EnsureCreated();
        base.ChangeTracker.AutoDetectChangesEnabled = false;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"FileName={_pathProvider.GetDatabasePath()}");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Currency>().Property(currency => currency.IsHide).HasDefaultValue(false);
        modelBuilder.Entity<DailyRate>().HasKey(rate => rate.Id);
        modelBuilder.Entity<Currency>().HasKey(currency => currency.Id);
        modelBuilder.Entity<SettingsEntity>().HasKey(currency => currency.Id);

        modelBuilder.Entity<DailyRate>()
            .HasMany(rate => rate.Currency)
            .WithOne(currency => currency.DailyRate)
            .HasForeignKey(currency => currency.DailyRateId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Currency> Currencies => Set<Currency>();
    public DbSet<DailyRate> DailyRates => Set<DailyRate>();
    public DbSet<SettingsEntity> SettingsEntities => Set<SettingsEntity>();

    public async Task<int> SaveChangesAsync()
    {
        try
        {
            var saveChangesAsync = await base.SaveChangesAsync();
            return saveChangesAsync;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task<List<T>> GetAll<T>() where T : BaseEntity => base.Set<T>().ToListAsync();
}
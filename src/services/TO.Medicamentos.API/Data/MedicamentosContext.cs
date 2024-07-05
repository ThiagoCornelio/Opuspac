using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using TO.Medicamentos.API.Models;
using TO.Core.Data;
using TO.Core.Messages;

namespace TO.Medicamentos.API.Data;

public class MedicamentosContext : DbContext, IUnitOfWork
{
    public MedicamentosContext(DbContextOptions<MedicamentosContext> options) : base(options) { }
    public DbSet<Medicamento> Medicamentos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<ValidationResult>();
        modelBuilder.Ignore<Event>();

        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
            e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MedicamentosContext).Assembly);
    }

    public async Task<bool> Commit() => await base.SaveChangesAsync() > 0;
}


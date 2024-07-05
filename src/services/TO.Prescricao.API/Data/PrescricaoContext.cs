using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using TO.Prescricao.API.Model;

namespace TO.Prescricao.API.Data;

public sealed class PrescricaoContext : DbContext
{
    public PrescricaoContext(DbContextOptions<PrescricaoContext> options)
        : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public DbSet<PrescricaoMedicamento> PrescricaoMedicamentos { get; set; }
    public DbSet<PrescricaoPaciente> PrescricaoPaciente { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
            e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        modelBuilder.Ignore<ValidationResult>();

        modelBuilder.Entity<PrescricaoPaciente>();

        modelBuilder.Entity<PrescricaoPaciente>()
            .HasMany(c => c.Itens)
            .WithOne(i => i.PrescricaoPaciente)
            .HasForeignKey(c => c.PrescricaoId);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PrescricaoContext).Assembly);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
    }
}
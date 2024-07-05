using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TO.Medicamentos.API.Models;

namespace TO.Medicamentos.API.Data.Mappings;

public class MedicamentoMapping : IEntityTypeConfiguration<Medicamento>
{
    public void Configure(EntityTypeBuilder<Medicamento> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Nome)
            .IsRequired()
            .HasColumnType("varchar(250)");

        builder.Property(c => c.Descricao)
            .IsRequired()
            .HasColumnType("varchar(500)");

        builder.ToTable("Medicamentos");
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TO.Prescricao.API.Model;

namespace TO.Prescricao.API.Data.Mapping;

public class PrescricaoMedicamentoMapping : IEntityTypeConfiguration<PrescricaoMedicamento>
{
    public void Configure(EntityTypeBuilder<PrescricaoMedicamento> builder)
    {
        builder.Property(pc => pc.QRCode)
            .IsRequired(false)
            .HasColumnType("varchar(max)");
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TO.Core.DomainObjects;
using TO.Funcionarios.API.Models;

namespace TO.Funcionarios.API.Data.Mappings;

public class FuncionarioMapping : IEntityTypeConfiguration<Funcionario>
{
    public void Configure(EntityTypeBuilder<Funcionario> builder)
    {
        builder.ToTable("Funcionarios");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Nome)
            .IsRequired()
            .HasColumnType("varchar(200)");

        builder.OwnsOne(c => c.Cpf, tf =>
        {
            tf.Property(c => c.Numero)
                .IsRequired()
                .HasMaxLength(Cpf.CpfMaxLength)
                .HasColumnName("Cpf")
                .HasColumnType($"varchar({Cpf.CpfMaxLength})");
        });

        builder.OwnsOne(c => c.Email, tf =>
        {
            tf.Property(c => c.Endereco)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType($"varchar({Email.EnderecoMaxLength})");
        });
    }
}
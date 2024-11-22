using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Med.dominio.ModuloAtividade;
using Med.dominio.ModuloMedico;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Med.Infra.Orm.ModuloMedico
{
    public class MapMedicoOrm : IEntityTypeConfiguration<Medico>
    {
        public void Configure(EntityTypeBuilder<Medico> builder)
        {
            builder.ToTable("Medicos");

            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(m => m.Nome).IsRequired().HasMaxLength(200);

            builder.Property(m => m.CRM).IsRequired().HasMaxLength(20);

            builder.Property(m => m.Email).HasMaxLength(150);

            builder.HasIndex(m => m.Email).IsUnique();

            builder.Property(m => m.Telefone).HasMaxLength(15);

            builder.Property(m => m.Cooldown).IsRequired();

        }
    }
}

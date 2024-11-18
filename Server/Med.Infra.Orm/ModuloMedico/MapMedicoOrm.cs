using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Med.dominio.ModuloMedico;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Med.Infra.Orm.ModuloMedico
{
    public class MapMedicoOrm : IEntityTypeConfiguration<Medico>
    {
        public void Configure(EntityTypeBuilder<Medico> builder)
        {
            builder.ToTable("TBMedico");

            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.Nome).IsRequired();

            builder.Property(x => x.Email).IsRequired();

            builder.Property(x => x.Telefone).IsRequired();

            builder.Property(x => x.CRM).IsRequired();

            builder.Property(x => x.Cooldown).IsRequired();

        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Med.dominio.ModuloCirurgia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Med.Infra.Orm.ModuloCirurgia
{
    public class MapCirurgiaOrm : IEntityTypeConfiguration<Cirurgia>
    {
        public void Configure(EntityTypeBuilder<Cirurgia> builder)
        {
            builder.ToTable("TBConsulta");

            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Titulo).IsRequired();
            builder.Property(x => x.Inicio).IsRequired();
            builder.Property(x => x.Termino).IsRequired();
            builder.HasOne(x => x.Medicos)
                .WithMany(x => x.)
                .HasForeignKey(x => x.Medico.Id)
                .HasConstraintName("FK_TBMedico_TBMedico")
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

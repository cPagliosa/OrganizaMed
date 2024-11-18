using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Med.dominio.ModuloConsulta;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Med.Infra.Orm.ModuloConsulta
{
    public class MapConsultaOrm : IEntityTypeConfiguration<Consulta>
    {
        public void Configure(EntityTypeBuilder<Consulta> builder)
        {
            builder.ToTable("TBConsulta");

            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Titulo).IsRequired();
            builder.Property(x => x.Inicio).IsRequired();
            builder.Property(x => x.Termino).IsRequired();
            builder.HasOne(x => x.Medico)
                .WithMany(x => x.Atividades)
                .HasForeignKey(x => x.Medico.Id)
                .HasConstraintName("FK_TBMedico_TBMedico")
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

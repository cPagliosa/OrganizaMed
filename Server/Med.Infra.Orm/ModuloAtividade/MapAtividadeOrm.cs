using Med.dominio.ModuloAtividade;
using Med.dominio.ModuloMedico;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Med.Infra.Orm.ModuloAtividade
{
    public class MapAtividadeOrm : IEntityTypeConfiguration<Atividade>
    {
        public void Configure(EntityTypeBuilder<Atividade> builder)
        {
            builder.ToTable("Ativiades");

            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.Titulo).IsRequired().HasMaxLength(200);

            builder.Property(x => x.Inicio).IsRequired();

            builder.Property(x => x.Termino).IsRequired();

            builder.Property(x => x.Tipo).IsRequired();

            builder.HasMany(x => x.Medicos)
                .WithMany(y => y.Atividades) 
                .UsingEntity<Dictionary<string, object>>(
                    "AtividadeMedico",
                    j => j.HasOne<Medico>().WithMany().HasForeignKey("MedicoId"),
                    j => j.HasOne<Atividade>().WithMany().HasForeignKey("AtividadeId")
                );
        }
    }
}

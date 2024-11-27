using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using Med.dominio.ModuloAtividade;
using Med.dominio.ModuloMedico;
using System;
using Med.dominio.Compartilhado;

namespace Med.Infra.Orm.Compartinhado
{
    public class MedDbContext : DbContext , IContextoPersistencia
    {
        public DbSet<Atividade> Atividades { get; set; }
        public DbSet<Medico> Medicos { get; set; }

        public MedDbContext(DbContextOptions<MedDbContext> options) : base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ILoggerFactory loggerFactory = LoggerFactory.Create((x) =>
            {
                x.AddSerilog(Log.Logger);
            });

            optionsBuilder.UseLoggerFactory(loggerFactory);

            optionsBuilder.EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração de relacionamento n para n
            modelBuilder.Entity<Atividade>()
                .HasMany(a => a.Medicos)
                .WithMany(m => m.Atividades)
                .UsingEntity(j => j.ToTable("AtividadesMedicos"));

         
        }

        public void DesfazerAlteracoes()
        {
            var registrosAlterados = ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Unchanged)
                .ToList();

            foreach (var registro in registrosAlterados)
            {
                switch (registro.State)
                {
                    case EntityState.Added:
                        registro.State = EntityState.Detached;
                        break;

                    case EntityState.Deleted:
                        registro.State = EntityState.Unchanged;
                        break;

                    case EntityState.Modified:
                        registro.State = EntityState.Unchanged;
                        registro.CurrentValues.SetValues(registro.OriginalValues);
                        break;

                    default:
                        break;
                }
            }
        }

        public void GravarDados()
        {
            SaveChanges();
        }

        public async Task<bool> GravarAsync()
        {
            int registrosAfetados = await SaveChangesAsync();

            return registrosAfetados > 0;
        }

        
    }
}

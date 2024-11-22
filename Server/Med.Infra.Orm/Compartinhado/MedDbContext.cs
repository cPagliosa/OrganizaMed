using eAgenda.Dominio.ModuloAutenticacao;
using Med.dominio.Compartilhado;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Reflection;
using Med.dominio.ModuloAtividade;
using Med.dominio.ModuloMedico;

namespace Med.Infra.Orm.Compartinhado
{
    public class MedDbContext : IdentityDbContext<Usuario, IdentityRole<Guid>, Guid>, IContextoPersistencia
    {
        private Guid usuarioId;

        public MedDbContext(DbContextOptions options,ITenantProvider tenantProvider = null) : base(options)
        {
            if(tenantProvider != null)
                usuarioId = tenantProvider.UsuarioId;
        }

        public MedDbContext() { }

        public void GravarDados()
        {
            SaveChanges();
        }

        public async Task<bool> GravarAsync()
        {
            int registrosAfetados = await SaveChangesAsync();

            return registrosAfetados > 0;
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
            Type tipo = typeof(MedDbContext);

            Assembly dllComConfiguracoesOrm = tipo.Assembly;

            modelBuilder.ApplyConfigurationsFromAssembly(dllComConfiguracoesOrm);

            modelBuilder.Entity<Medico>().HasQueryFilter(x => x.UsuarioId == usuarioId);
            modelBuilder.Entity<Atividade>().HasQueryFilter(x => x.UsuarioId == usuarioId);
            

            base.OnModelCreating(modelBuilder);
        }
    }
}

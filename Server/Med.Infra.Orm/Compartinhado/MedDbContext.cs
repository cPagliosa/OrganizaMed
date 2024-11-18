using Med.dominio.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace Med.Infra.Orm.Compartinhado
{
    public class MedDbContext : DbContext,IContextoPersistencia
    {
        public MedDbContext(DbContextOptions options) : base(options) { }

        public Task<bool> GravarAsync()
        {
            throw new NotImplementedException();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration()

            base.OnModelCreating(modelBuilder);
        }
    }
}

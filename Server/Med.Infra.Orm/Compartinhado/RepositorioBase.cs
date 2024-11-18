using Med.dominio.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace Med.Infra.Orm.Compartinhado
{
    public class RepositorioBase<TEntidade> where TEntidade : EntidadeBase
    {
        protected MedDbContext dbContext;
        protected DbSet<TEntidade> registros;

        public RepositorioBase(IContextoPersistencia ctx)
        {
            this.dbContext = (MedDbContext)ctx;
            this.registros = dbContext.Set<TEntidade>();
        }

        public async Task<bool> InserirAsync(TEntidade registro)
        {
            await registros.AddAsync(registro);

            await dbContext.SaveChangesAsync();

            return true;
        }

        public virtual TEntidade SelecionarPorId(Guid id)
        {
            return registros.SingleOrDefault(x => x.Id == id);
        }

        public async virtual Task<TEntidade> SelecionarPorIdAsync(Guid id)
        {
            return await registros.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<TEntidade>> SelecionarTodosAsync()
        {
            return await registros.ToListAsync();
        }
    }
}

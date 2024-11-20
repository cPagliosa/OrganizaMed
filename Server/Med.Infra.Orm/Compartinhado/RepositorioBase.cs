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

        public virtual void Inserir(TEntidade novoRegistro)
        {
            registros.Add(novoRegistro);
        }

        public virtual TEntidade SelecionarPorId(Guid id)
        {
            return registros.SingleOrDefault(x => x.Id == id);
        }

        public virtual List<TEntidade> SelecionarTodos()
        {
            return registros.ToList();
        }

        public virtual async Task<bool> InserirAsync(TEntidade novoRegistro)
        {
            await registros.AddAsync(novoRegistro);
            return true;
        }

        public virtual async Task<TEntidade> SelecionarPorIdAsync(Guid id)
        {
            return await registros.SingleOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<List<TEntidade>> SelecionarTodosAsync()
        {
            return await registros.ToListAsync();
        }
    }
}

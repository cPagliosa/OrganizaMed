using Med.dominio.Compartilhado;
using Med.dominio.ModuloAtividade;
using Med.Infra.Orm.Compartinhado;

namespace Med.Infra.Orm.ModuloAtividade
{
    public class RepositorioAtividedeOrm : RepositorioBase<Atividade>,IRepositorioAtividade
    {
        public RepositorioAtividedeOrm(IContextoPersistencia ctx) : base(ctx)
        {
        }
    }
}

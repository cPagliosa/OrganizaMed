using Med.dominio.Compartilhado;
using Med.dominio.ModuloMedico;
using Med.Infra.Orm.Compartinhado;
using Microsoft.EntityFrameworkCore;

namespace Med.Infra.Orm.ModuloMedico
{
    public class RepositorioMedicoOrm : RepositorioBase<Medico>,IRepositorioMedico
    {
        public RepositorioMedicoOrm(IContextoPersistencia ctx) : base(ctx)
        {
        }

    }
}

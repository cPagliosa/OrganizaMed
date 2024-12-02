using Med.dominio.Compartilhado;
using Med.dominio.ModuloMedico;
using Med.Infra.Orm.Compartinhado;

namespace Med.Infra.Orm.ModuloMedico
{
    public class RepositorioMedicoOrm : RepositorioBase<Medico>,IRepositorioMedico
    {
        public RepositorioMedicoOrm(IContextoPersistencia ctx) : base(ctx)
        {
        }


        public List<Medico> SelecionarMuitos(List<Guid> idsAtividadesSelecionadas)
        {
            return registros.Where(medi => idsAtividadesSelecionadas.Contains(medi.Id)).ToList();
        }
    }
}

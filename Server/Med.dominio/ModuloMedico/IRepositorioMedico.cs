using Med.dominio.Compartilhado;

namespace Med.dominio.ModuloMedico
{
    public interface IRepositorioMedico : IRepositorioBase<Medico>
    {
        List<Medico> SelecionarMuitos(List<Guid> idsAtividadesSelecionadas);
    }
}

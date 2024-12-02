using Med.dominio.Compartilhado;
using Med.dominio.ModuloMedico;
using Microsoft.Win32;

namespace Med.dominio.ModuloAtividade
{
    public interface IRepositorioAtividade : IRepositorioBase<Atividade>
    {
        Task<List<Atividade>> SelecionarTodaAtividade();

    }
}

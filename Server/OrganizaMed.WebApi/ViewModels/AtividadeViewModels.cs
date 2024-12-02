using Med.dominio.ModuloAtividade;
using Med.dominio.ModuloMedico;

namespace OrganizaMed.WebApi.ViewModels
{
    public class InserirAtividadeViewModels
    {
        public required string Titulo { get; set; }
        public required DateTime Inicio { get; set; }
        public required DateTime Termino { get; set; }
        public required TipoAtividade Tipo { get; set; }
        public required List<Guid> MedicosId { get; set; }

    }

    public class EditarAtividadeViewModel
    {
        public required string Titulo { get; set; }
        public required DateTime Inicio { get; set; }
        public required DateTime Termino { get; set; }
        public required TipoAtividade Tipo { get; set; }
        public required List<ListarMedicoViewModel> Medicos { get; set; }
    }

    public class ListarAtividadeViewModel
    {
        public required Guid Id { get; set; }
        public required string Titulo { get; set; }
        public required DateTime Inicio { get; set; }
        public required DateTime Termino { get; set; }
        public required TipoAtividade Tipo { get; set; }
        public required List<ListarMedicoViewModel> Medicos { get; set; }
    }

    public class VisualizarAtividadeViewModel
    {
        public required Guid Id { get; set; }
        public required string Titulo { get; set; }
        public required DateTime Inicio { get; set; }
        public required DateTime Termino { get; set; }
        public required TipoAtividade Tipo { get; set; }
        public required List<ListarMedicoViewModel> Medicos { get; set; }
    }
}

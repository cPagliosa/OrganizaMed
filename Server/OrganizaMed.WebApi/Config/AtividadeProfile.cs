using AutoMapper;
using Med.dominio.ModuloAtividade;
using Med.dominio.ModuloMedico;
using OrganizaMed.WebApi.ViewModels;

namespace OrganizaMed.WebApi.Config
{
    public class AtividadeProfile: Profile
    {
        public AtividadeProfile()
        {
            CreateMap<Atividade, ListarAtividadeViewModel>();
            CreateMap<Atividade, VisualizarAtividadeViewModel>();

            CreateMap<InserirAtividadeViewModels, Atividade>();
            CreateMap<EditarAtividadeViewModel, Atividade>();
        }
    }
}

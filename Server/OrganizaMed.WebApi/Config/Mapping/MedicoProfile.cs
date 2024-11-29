using AutoMapper;
using Med.dominio.ModuloMedico;
using OrganizaMed.WebApi.ViewModels;

namespace OrganizaMed.WebApi.Config.Mapping
{
    public class MedicoProfile : Profile
    {
        public MedicoProfile()
        {
            CreateMap<Medico, ListarMedicoViewModel>();
            CreateMap<Medico, VisualizarMedicoViewModel>();

            CreateMap<InserirMedicoViewModels, Medico>();
            CreateMap<EditarMedicoViewModel, Medico>();
        }
    }
}

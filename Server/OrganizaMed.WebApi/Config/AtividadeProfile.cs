using AutoMapper;
using Med.dominio.ModuloAtividade;
using Med.dominio.ModuloMedico;
using OrganizaMed.WebApi.ViewModels;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OrganizaMed.WebApi.Config
{
    public class AtividadeProfile: Profile
    {
        public AtividadeProfile()
        {

            CreateMap<Atividade, ListarAtividadeViewModel>();
            CreateMap<Atividade, VisualizarAtividadeViewModel>();

            CreateMap<InserirAtividadeViewModels, Atividade>()
                .AfterMap<FormsAtividadeMappingAction>();

            CreateMap<EditarAtividadeViewModel, Atividade>();
        }

        public class FormsAtividadeMappingAction(IRepositorioMedico repositorioMedico) : IMappingAction<InserirAtividadeViewModels, Atividade>
        {
            public void Process(InserirAtividadeViewModels source, Atividade destination, ResolutionContext context)
            {
                var idmedico = source.MedicosId;
                foreach (Guid id in idmedico)
                {
                    Medico med = repositorioMedico.SelecionarPorId(id);
                    destination.Medicos.Add(med);
                }
            }
        }
    }
}

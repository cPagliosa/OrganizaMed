using AutoMapper;
using Med.dominio.ModuloMedico;
using Microsoft.AspNetCore.Mvc;
using OrganizaMed.Aplicacao.ModuloMedico;
using OrganizaMed.WebApi.ViewModels;

namespace OrganizaMed.WebApi.Controllers
{
    [Route("api/Medico")]
    [ApiController]
    public class MedicoController(ServicoMedico servicoMedico,IMapper mapeador) : ControllerBase
    {
        [HttpGet]
        
        public async Task<IActionResult> Get()
        {
            var resultado = await servicoMedico.SelecionarTodosAsync();

            if (resultado.IsFailed)
                return StatusCode(500);

            var viewModel = mapeador.Map<ListarMedicoViewModel[]>(resultado.Value);

            return Ok(viewModel);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var categoriaResult = await servicoMedico.SelecionarPorIdAsync(id);

            if (categoriaResult.IsFailed)
                return StatusCode(500);

            if (categoriaResult.IsSuccess && categoriaResult.Value is null)
                return NotFound(categoriaResult.Errors);

            var viewModel = mapeador.Map<VisualizarMedicoViewModel>(categoriaResult.Value);

            return Ok(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Post(InserirMedicoViewModels medicoVm)
        {
            var medico = mapeador.Map<Medico>(medicoVm);

            var resultado = await servicoMedico.InserirAsync(medico);

            if (resultado.IsFailed)
                return BadRequest(resultado.Errors);

            return Ok(medicoVm);
        }
    }
}

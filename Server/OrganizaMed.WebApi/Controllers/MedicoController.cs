using AutoMapper;
using Med.dominio.ModuloMedico;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganizaMed.Aplicacao.ModuloMedico;
using OrganizaMed.WebApi.ViewModels;

namespace OrganizaMed.WebApi.Controllers
{
    [Route("api/Medico")]
    [ApiController]
    public class MedicoController(ServicoMedico servicoMedico,IMapper mapeador) : ControllerBase
    {
        [HttpGet("SelecionarTodos")]
        
        public async Task<IActionResult> Get()
        {
            var resultado = await servicoMedico.SelecionarTodosAsync();

            if (resultado.IsFailed)
                return StatusCode(500);

            var viewModel = mapeador.Map<ListarMedicoViewModel[]>(resultado.Value);

            return Ok(viewModel);
        }

        [HttpGet("SelecionarPorId/{id}")]
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


        [HttpPost("Inserir")]
        public async Task<IActionResult> Post(InserirMedicoViewModels medicoVm)
        {
            var medico = mapeador.Map<Medico>(medicoVm);

            var resultado = await servicoMedico.InserirAsync(medico);

            if (resultado.IsFailed)
                return BadRequest(resultado.Errors);

            return Ok(medicoVm);
        }

        /*
        [HttpPut("Editar/{id}")]
        public async Task<IActionResult> Put(Guid id, EditarMedicoViewModel categoriaVm)
        {
            var selecaoCategoriaOriginal = await servicoMedico.SelecionarPorIdAsync(id);

            if (selecaoCategoriaOriginal.IsFailed)
                return NotFound(selecaoCategoriaOriginal.Errors);

            var categoriaEditada = mapeador.Map(categoriaVm, selecaoCategoriaOriginal.Value);

            var edicaoResult = await servicoMedico.EditarAsync(categoriaEditada);

            if (edicaoResult.IsFailed)
                return BadRequest(edicaoResult.Errors);

            return Ok(edicaoResult.Value);
        }
        */
    }
}

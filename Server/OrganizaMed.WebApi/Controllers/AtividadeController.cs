using AutoMapper;
using Med.dominio.ModuloAtividade;
using Microsoft.AspNetCore.Mvc;
using OrganizaMed.Aplicacao.ModuloAtividade;
using OrganizaMed.WebApi.ViewModels;

namespace OrganizaMed.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtividadeController(ServicoAtividade servicoAtividade,IMapper mapeador) : ControllerBase
    {
        [HttpGet("SelecionarTodos")]

        public async Task<IActionResult> Get()
        {
            var resultado = await servicoAtividade.SelecionarTodosAsync();

            if (resultado.IsFailed)
                return StatusCode(500);

            var viewModel = mapeador.Map<ListarAtividadeViewModel[]>(resultado.Value);

            return Ok(viewModel);
        }

        [HttpGet("SelecionarPorId/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var categoriaResult = await servicoAtividade.SelecionarPorIdAsync(id);

            if (categoriaResult.IsFailed)
                return StatusCode(500);

            if (categoriaResult.IsSuccess && categoriaResult.Value is null)
                return NotFound(categoriaResult.Errors);

            var viewModel = mapeador.Map<VisualizarAtividadeViewModel>(categoriaResult.Value);

            return Ok(viewModel);
        }

        [HttpPost("Inserir")]
        public async Task<IActionResult> Post(InserirAtividadeViewModels atividadeVm)
        {
            var atividade = mapeador.Map<Atividade>(atividadeVm);

            var resultado = await servicoAtividade.InserirAsync(atividade);

            if (resultado.IsFailed)
                return BadRequest(resultado.Errors);

            return Ok(atividadeVm);
        }
    }
}

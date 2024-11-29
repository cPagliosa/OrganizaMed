using FluentResults;
using Med.dominio.Compartilhado;
using Med.dominio.ModuloMedico;
using OrganizaMed.Aplicacao.Compartinhado;
using Serilog;

namespace OrganizaMed.Aplicacao.ModuloMedico
{
    public class ServicoMedico : ServicoBase<Medico,ValidadorMedico>
    {
        private IRepositorioMedico repositorioMedico;
        private IContextoPersistencia contextoPersistencia;

        public ServicoMedico(IRepositorioMedico repositorioMedico, IContextoPersistencia contextoPersistencia)
        {
            this.repositorioMedico = repositorioMedico;
            this.contextoPersistencia = contextoPersistencia;
        }

        public async Task<Result<Medico>> InserirAsync(Medico mediico)
        {
            Result resultado = Validar(mediico);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            await repositorioMedico.InserirAsync(mediico);

            await contextoPersistencia.GravarAsync();

            return Result.Ok(mediico);
        }

        public async Task<Result<List<Medico>>> SelecionarTodosAsync()
        {
            var medicos = await repositorioMedico.SelecionarTodosAsync();

            return Result.Ok(medicos);
        }

        public async Task<Result<Medico>> SelecionarPorIdAsync(Guid id)
        {
            var medico = await repositorioMedico.SelecionarPorIdAsync(id);

            if (medico == null)
            {
                Log.Logger.Warning("Medico {MedicoId} não encontrado", id);

                return Result.Fail($"Medico {id} não encontrado");
            }

            return Result.Ok(medico);
        }

        public async Task<Result<Medico>> EditarAsync(Medico medico)
        {
            var resultadoValidacao = medico.Validar();

            if (resultadoValidacao.Count > 0)
                return Result.Fail(resultadoValidacao);

            repositorioMedico.Editar(medico);

            return Result.Ok(medico);
        }
    }
}

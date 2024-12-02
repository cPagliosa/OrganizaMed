using System.Runtime.CompilerServices;
using FluentResults;
using Med.dominio.Compartilhado;
using Med.dominio.ModuloAtividade;
using Med.dominio.ModuloMedico;
using OrganizaMed.Aplicacao.Compartinhado;
using Serilog;

namespace OrganizaMed.Aplicacao.ModuloAtividade
{
    public class ServicoAtividade : ServicoBase<Atividade,ValidarAtividade>
    {
        private IContextoPersistencia contextoPersistencia;
        private IRepositorioAtividade repositorioAtividade;
        private IRepositorioMedico repositorioMedico;

        public ServicoAtividade(IRepositorioAtividade repositorioAtividade,IRepositorioMedico repositorioMedico,IContextoPersistencia contextoPersistencia)
        {
            this.repositorioAtividade = repositorioAtividade;
            this.repositorioMedico = repositorioMedico;
            this.contextoPersistencia = contextoPersistencia;
        }

        public async Task<Result<Atividade>> InserirAsync(Atividade atividade)
        {
            Result resultado = Validar(atividade);

            //this.ValidarTempoAtividade(atividade);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            this.AddColdownMedico(atividade);

            await repositorioAtividade.InserirAsync(atividade);

            await contextoPersistencia.GravarAsync();

            return Result.Ok(atividade);
        }

        public async Task<Result<List<Atividade>>> SelecionarTodosAsync()
        {
            var atividades = await repositorioAtividade.SelecionarTodosAsync();

            return Result.Ok(atividades);
        }

        public async Task<Result<Atividade>> SelecionarPorIdAsync(Guid id)
        {
            var atividade = await repositorioAtividade.SelecionarPorIdAsync(id);

            if (atividade == null)
            {
                Log.Logger.Warning("Atividade {AtividadeId} não encontrado", id);

                return Result.Fail($"Atividade {id} não encontrado");
            }

            return Result.Ok(atividade);
        }

        private async void ValidarTempoAtividade(Atividade atividade)
        {
            DateTime  cool= atividade.Medicos[0].Cooldown;

            // Obtém todas as atividades existentes
            var atividades = await repositorioAtividade.SelecionarTodaAtividade();

            atividade.Medicos[0].Cooldown = cool;

            foreach (var ats in atividades)
            {
                // Verifica se há interseção de médicos entre as atividades
                var medicosEmComum = ats.Medicos.Intersect(atividade.Medicos);
                if (medicosEmComum.Any())
                {
                    // Verifica se os horários das atividades se sobrepõem
                    if (atividade.Inicio < ats.Termino && atividade.Termino > ats.Inicio)
                    {
                        throw new InvalidOperationException($"Conflito de horário detectado para o(s) médico(s): {string.Join(", ", medicosEmComum.Select(m => m.Nome))}");
                    }
                }
            }

            // Valida se o médico está em cooldown
            foreach (var medico in atividade.Medicos)
            {
                if (atividade.Inicio < medico.Cooldown)
                {
                    throw new InvalidOperationException($"O médico {medico.Nome} está em cooldown até {medico.Cooldown:dd/MM/yyyy HH:mm} e não pode iniciar uma nova atividade antes desse horário.");
                }
            }
        }

        private void AddColdownMedico(Atividade atividade)
        {

            List<Medico> medicos = atividade.Medicos;

            foreach (var m in medicos)
            {
                if (atividade.Tipo == TipoAtividade.Cirurgia)
                {
                    m.Cooldown = atividade.Termino.AddHours(4);
                }
                else
                {
                    m.Cooldown = atividade.Termino.AddMinutes(10);
                }

                repositorioMedico.Editar(m);

            }
        }
    }
}

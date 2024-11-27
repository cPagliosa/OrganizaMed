using Med.dominio.Compartilhado;
using Med.dominio.ModuloAtividade;
using Med.dominio.ModuloAtividadeMedico;
using Med.dominio.ModuloMedico;
using Med.Infra.Orm.Compartinhado;
using Med.Infra.Orm.ModuloMedico;

namespace Med.Infra.Orm.ModuloAtividade
{
    public class RepositorioAtividedeOrm : RepositorioBase<Atividade>,IRepositorioAtividade
    {
        private RepositorioMedicoOrm repositorioMedico;
        public RepositorioAtividedeOrm(IContextoPersistencia ctx) : base(ctx)
        {
        }

        public override void Inserir(Atividade novoRegistro)
        {
            ValidarAtividade(novoRegistro);
            ValidarTempoAtividade(novoRegistro);
            AddColdownMedico(novoRegistro);
            base.Inserir(novoRegistro);
        }

        private void ValidarAtividade(Atividade atividade)
        {
            if (atividade.Tipo == TipoAtividade.Consulta)
            {
                if (atividade.Medicos.Count() != 1)
                {
                    throw new InvalidOperationException("Uma Consulta deve ter exatamente um médico.");
                }
            }
            else // Outros tipos de atividades
            {
                if (!atividade.Medicos.Any())
                {
                    throw new InvalidOperationException("Uma atividade do tipo especificado deve ter pelo menos um médico.");
                }
            }
        }

        private void ValidarTempoAtividade(Atividade atividade)
        {
            // Obtém todas as atividades existentes
            List<Atividade> atividades = SelecionarTodos();

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
            repositorioMedico = new RepositorioMedicoOrm(dbContext);

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

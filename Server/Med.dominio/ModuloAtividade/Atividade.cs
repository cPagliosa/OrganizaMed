using Med.dominio.Compartilhado;
using Med.dominio.ModuloMedico;

namespace Med.dominio.ModuloAtividade
{
    public enum TipoAtividade
    {
        Cirurgia,
        Consulta
    }
    public class Atividade : EntidadeBase<Atividade>
    {
        public string Titulo { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Termino { get; set; }
        public List<Medico> Medicos { get; set; }
        public TipoAtividade Tipo { get; set; }

        public Atividade() { }

        public Atividade(string titulo,DateTime inicio,DateTime termino,List<Medico> medicos,bool tipo)
        {
            this.Titulo = titulo;
            this.Inicio = inicio;
            this.Termino = termino;
            this.Medicos = medicos;
            if (tipo) this.Tipo = TipoAtividade.Consulta;
            else this.Tipo = TipoAtividade.Cirurgia;
        }

        public override void Atualizar(Atividade registro)
        {
            Id = registro.Id;
            Titulo = registro.Titulo;
            Termino = registro.Termino;
            Medicos = registro.Medicos;
            Tipo = registro.Tipo;
        }

        public override List<string> Validar()
        {
            List<string> erros = new List<string>();

            if (string.IsNullOrWhiteSpace(Titulo))
                erros.Add("O título da atividade não pode ser vazio.");

            if (Inicio == default)
                erros.Add("A data de início deve ser válida.");

            if (Termino == default)
                erros.Add("A data de término deve ser válida.");

            if (Inicio >= Termino)
                erros.Add("A data de término deve ser posterior à data de início.");

            if (Medicos == null || Medicos.Count == 0)
                erros.Add("A atividade deve ter pelo menos um médico associado.");

            switch (Tipo)
            {
                case TipoAtividade.Consulta:
                    if (Medicos.Count != 1)
                        erros.Add("Na consulta, é obrigatório ter exatamente um médico.");
                    break;

                case TipoAtividade.Cirurgia:
                    if (Medicos.Count < 1)
                        erros.Add("Na cirurgia, é obrigatório ter pelo menos um médico.");
                    break;

                default:
                    erros.Add("Tipo de atividade inválido.");
                    break;
            }

            return erros;
        }
    }
}

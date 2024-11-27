using Med.dominio.Compartilhado;
using Med.dominio.ModuloAtividadeMedico;
using Med.dominio.ModuloMedico;

namespace Med.dominio.ModuloAtividade
{
    public enum TipoAtividade
    {
        Cirurgia = 0,
        Consulta = 1
    }
    public class Atividade : EntidadeBase<Atividade>
    {
        public string Titulo { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Termino { get; set; }
        public TipoAtividade Tipo { get; set; }
        public List<Medico> Medicos { get; set; } = new List<Medico>();

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

            return erros;
        }
    }
}

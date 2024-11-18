using Med.dominio.ModuloAtividade;
using Med.dominio.ModuloMedico;

namespace Med.dominio.ModuloCirurgia
{
    public class Cirurgia : Atividade
    {
        public List<Medico> Medicos { get; set; }
        public TimeSpan Descanco = TimeSpan.FromHours(4);

        public Cirurgia() { }

        public Cirurgia(string titulo, List<Medico> medicos, DateTime inicio, DateTime termino)
        {
            this.Titulo = titulo;
            this.Medicos = medicos;
            this.Inicio = inicio;
            this.Termino = termino;
        }

        public override List<string> Validar()
        {
            List<string> erros = new List<string>();

            if (string.IsNullOrEmpty(this.Titulo))
                erros.Add("O título não pode ser nulo");

            if (this.Medicos == null || this.Medicos.Count == 0)
                erros.Add("É necessário pelo menos um médico para realizar a cirurgia");

            if (this.Inicio == default(DateTime))
                erros.Add("Data de início inválida");

            if (this.Termino == default(DateTime))
                erros.Add("Data de término inválida");

            if (this.Termino <= this.Inicio)
                erros.Add("A data de término deve ser posterior à data de início");

            return erros;
        }

        public bool VerificarConflito(Cirurgia novaCirurgia, List<Cirurgia> cirurgiasExistentes)
        {
            foreach (var cirurgia in cirurgiasExistentes)
            {
                if ((novaCirurgia.Inicio < cirurgia.Termino && novaCirurgia.Termino > cirurgia.Inicio))
                {
                    return true;
                }
            }

            return false;
        }
    }

}

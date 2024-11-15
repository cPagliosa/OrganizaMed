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
            List<string> erros = [];

            if (string.IsNullOrEmpty(this.Titulo))
                erros.Add("O titulo não pode ser nulo");

            if (this.Medicos.Count > 0)
                erros.Add("Tem que ter um medio para realizar a consulta");

            if (string.IsNullOrEmpty(this.Inicio.ToLongDateString()))
                erros.Add("Data de inicio invalida");

            if (string.IsNullOrEmpty(this.Termino.ToLongDateString()))
                erros.Add("Data de termino invalida");

            return erros;
        }

    }
}

using Med.dominio.ModuloAtividade;
using Med.dominio.ModuloMedico;

namespace Med.dominio.ModuloConsulta
{
    public class Consulta : Atividade
    {

        public Medico Medico { get; set; }

        public TimeSpan Descanco = TimeSpan.FromMinutes(10);
        public Consulta() { }

        public Consulta(string titulo,Medico medico,DateTime inicio,DateTime termino)
        {
            this.Titulo = titulo;
            this.Medico = medico;
            this.Inicio = inicio;
            this.Termino = termino;
        }
        public override List<string> Validar()
        {
            List<string> erros = [];

            if (string.IsNullOrEmpty(this.Titulo))
                erros.Add("O titulo não pode ser nulo");

            if (this.Medico.Equals(null))
                erros.Add("Tem que ter um medio para realizar a consulta");

            if (string.IsNullOrEmpty(this.Inicio.ToLongDateString()))
                erros.Add("Data de inicio invalida");

            if (string.IsNullOrEmpty(this.Termino.ToLongDateString()))
                erros.Add("Data de termino invalida");

            if (this.Termino <= this.Inicio)
                erros.Add("A data de término deve ser posterior à data de início");

            return erros;
        }

        public bool VerificarConflito(Consulta novaConsulta, List<Consulta> consultasExistentes)
        {
            foreach (var consulta in consultasExistentes)
            {
                if ((novaConsulta.Inicio < consulta.Termino && novaConsulta.Termino > consulta.Inicio))
                {
                    return true;
                }
            }

            return false;
        }
    }
}

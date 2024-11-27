using Med.dominio.Compartilhado;
using Med.dominio.ModuloAtividade;
using Med.dominio.ModuloAtividadeMedico;

namespace Med.dominio.ModuloMedico
{
    [Serializable]
    public class Medico : EntidadeBase<Medico>
    {
        public string Nome { get; set; }
        public string CRM { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime Cooldown { get; set; }
        public List<Atividade> Atividades { get; set; } = new List<Atividade>();


        public Medico() { }

        public Medico(string nome, string crm, string email, string telefone, DateTime? cooldown = null)
        {
            this.Nome = nome;
            this.CRM = crm;
            this.Email = email;
            this.Telefone = telefone;
            this.Cooldown = cooldown ?? DateTime.MinValue;
        }

        public override void Atualizar(Medico registro)
        {
            Id = registro.Id;
            Nome = registro.Nome;
            CRM = registro.CRM;
            Email = registro.Email;
            Telefone = registro.Telefone;
            Cooldown = registro.Cooldown;
        }

        public override List<string> Validar()
        {
            List<string> erros = [];

            if(this.Nome.Length < 2)
                erros.Add("O nome precisa ter mais de dois caracteres");

            if(this.CRM.Length != 8)
                erros.Add("O CRM invalido");

            if(string.IsNullOrEmpty(this.Email))
                erros.Add("O email não pode ser nulo");

            if (string.IsNullOrEmpty(this.Telefone))
                erros.Add("O telefone não pode ser nulo");

            return erros;
        }

        public DateTime Descanso(TimeSpan tempo,DateTime termino)
        {
            DateTime fimDescanso = termino.Add(tempo);

            return fimDescanso;
        }
    }
}

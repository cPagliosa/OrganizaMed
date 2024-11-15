using Med.dominio.Compartilhado;

namespace Med.dominio.ModuloMedico
{
    public class Medico : EntidadeBase
    {
        public string Nome { get; set; }
        public string CRM { get; set; }
        public DateTime DataUltimaAtividade { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        public Medico() { }

        public Medico(string nome,string crm,string email,string telefone,DateTime UltimaAtividade)
        {
            this.Nome = nome;
            this.CRM = crm;
            this.Email = email;
            this.Telefone = telefone;
            this.DataUltimaAtividade = UltimaAtividade;
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

            if (string.IsNullOrEmpty(this.Email))
                erros.Add("O email não pode ser nulo");

            return erros;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Med.dominio.Compartilhado;
using Med.dominio.ModuloAtividade;
using Med.dominio.ModuloMedico;

namespace Med.dominio.ModuloAtividadeMedico
{
    public class AtividadeMedico : EntidadeBase<AtividadeMedico>
    {
        public Guid IdAtividade { get; set; }
        public Atividade Atividade { get; set; }
        public Guid IdMedico { get; set; }
        public Medico Medico { get; set; }

        public AtividadeMedico() { }

        public AtividadeMedico(Guid medico,Guid atividade)
        {
            IdMedico = medico;
            IdAtividade = atividade;
        }
        public override void Atualizar(AtividadeMedico registro)
        {
            this.IdMedico = registro.IdMedico;
            this.IdAtividade = registro.IdAtividade;
        }

        public override List<string> Validar()
        {
            throw new NotImplementedException();
        }
    }
}

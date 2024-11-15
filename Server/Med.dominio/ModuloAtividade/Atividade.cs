using Med.dominio.Compartilhado;

namespace Med.dominio.ModuloAtividade
{
    public abstract class Atividade : EntidadeBase
    {
        public string Titulo { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Termino { get; set; }

    }
}

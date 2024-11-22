using eAgenda.Dominio.ModuloAutenticacao;

namespace Med.dominio.Compartilhado
{
    public abstract class EntidadeBase<T>
    {
        public Guid Id { get; set; }

        public EntidadeBase()
        {
            this.Id = Guid.NewGuid();
        }
        public Usuario Usuario { get; set; }
        public Guid UsuarioId { get; set; }

        public abstract void Atualizar(T registro);
        public abstract List<string> Validar();
    }
}

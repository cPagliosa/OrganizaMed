namespace Med.dominio.Compartilhado
{
    public abstract class EntidadeBase
    {
        public Guid Id { get; set; }

        public EntidadeBase()
        {
            this.Id = Guid.NewGuid();
        }

        public abstract List<string> Validar();
    }
}

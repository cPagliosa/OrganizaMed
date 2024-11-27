namespace Med.dominio.Compartilhado
{
    public abstract class EntidadeBase<T>
    {
        public Guid Id { get; set; }

        public EntidadeBase()
        {
            this.Id = Guid.NewGuid();
        }
        public abstract void Atualizar(T registro);
        public abstract List<string> Validar();
    }
}

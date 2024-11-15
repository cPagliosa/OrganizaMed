namespace Med.dominio.Compartilhado
{
    public interface IRepositorioBase<TEntidadeBase> where TEntidadeBase : EntidadeBase
    {
        Task<bool> Inserir(TEntidadeBase registro);

        TEntidadeBase SelecionarPorId(Guid  id);

        Task<TEntidadeBase> SelecionarPorIdAsync(Guid id);

        Task<List<TEntidadeBase>> SelecionarTodos();

    }
}

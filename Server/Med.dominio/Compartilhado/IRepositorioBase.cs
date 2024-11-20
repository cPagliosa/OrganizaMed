namespace Med.dominio.Compartilhado
{
    public interface IRepositorioBase<TEntidadeBase> where TEntidadeBase : EntidadeBase
    {
        void Inserir(TEntidadeBase novoRegistro);

        List<TEntidadeBase> SelecionarTodos();

        TEntidadeBase SelecionarPorId(Guid id);

       
        Task<bool> InserirAsync(TEntidadeBase registro);

        Task<List<TEntidadeBase>> SelecionarTodosAsync();

        Task<TEntidadeBase> SelecionarPorIdAsync(Guid id);
    }
}

namespace ControleDeBar.Dominio.Compartilhado
{
    public interface IRepositorio<T> where T : EntidadeBase<T>
    {
        public void CadastrarRegistro(T novoRegistro);
        public void EditarRegistro(T idRegistro, T registroEditado);
        public void ExcluirRegistro(Guid idRegistro);
        public List<T> SelecionarRegistros();
        public T SelecionarRegistroPorId(Guid idRegistro);
    }
    
}

using ControleDeBar.Dominio.ModuloProduto;

namespace ControleDeBar.Infraestrutura.SqlServer.ModuloProduto
{
    public class RepositorioProdutoEmSql : IRepositorioProduto
    {
        public void CadastrarRegistro(Produto novoRegistro)
        {
            throw new NotImplementedException();
        }

        public bool EditarRegistro(Guid idRegistro, Produto registroEditado)
        {
            throw new NotImplementedException();
        }

        public bool ExcluirRegistro(Guid idRegistro)
        {
            throw new NotImplementedException();
        }

        public Produto SelecionarRegistroPorId(Guid idRegistro)
        {
            throw new NotImplementedException();
        }

        public List<Produto> SelecionarRegistros()
        {
            throw new NotImplementedException();
        }
    }
}

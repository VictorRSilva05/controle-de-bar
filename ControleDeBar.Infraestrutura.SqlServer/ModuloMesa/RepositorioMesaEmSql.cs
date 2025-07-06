using ControleDeBar.Dominio.ModuloMesa;

namespace ControleDeBar.Infraestrutura.SqlServer.ModuloMesa
{
    public class RepositorioMesaEmSql : IRepositorioMesa
    {
        public void CadastrarRegistro(Mesa novoRegistro)
        {
            throw new NotImplementedException();
        }

        public bool EditarRegistro(Guid idRegistro, Mesa registroEditado)
        {
            throw new NotImplementedException();
        }

        public bool ExcluirRegistro(Guid idRegistro)
        {
            throw new NotImplementedException();
        }

        public Mesa SelecionarRegistroPorId(Guid idRegistro)
        {
            throw new NotImplementedException();
        }

        public List<Mesa> SelecionarRegistros()
        {
            throw new NotImplementedException();
        }
    }
}

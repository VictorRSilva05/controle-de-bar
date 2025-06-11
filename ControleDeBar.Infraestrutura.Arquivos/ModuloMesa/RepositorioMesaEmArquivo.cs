using ControleDeBar.Dominio.Compartilhado;
using ControleDeBar.Dominio.ModuloMesa;
using ControleDeBar.Infraestrura.Arquivos.Compartilhado;

namespace ControleDeBar.Infraestrutura.Arquivos.ModuloMesa
{
    public class RepositorioMesaEmArquivo : RepositorioBaseEmArquivo<Mesa>, IRepositorioMesa
    {
        public RepositorioMesaEmArquivo(ContextoDados contexto) : base(contexto)
        {
        }

        public void EditarRegistro(Mesa idRegistro, Mesa registroEditado)
        {
            throw new NotImplementedException();
        }

        protected override List<Mesa> ObterRegistros()
        {
            return contexto.Mesas;
        }

        void IRepositorio<Mesa>.ExcluirRegistro(Guid idRegistro)
        {
            throw new NotImplementedException();
        }
    }
    
    
}

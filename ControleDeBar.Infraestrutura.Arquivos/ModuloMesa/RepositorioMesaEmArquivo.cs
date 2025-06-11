using ControleDeBar.Dominio.Compartilhado;
using ControleDeBar.Dominio.ModuloMesa;
using ControleDeBar.Infraestrura.Arquivos.Compartilhado;

namespace ControleDeBar.Infraestrura.Arquivos.ModuloMesa;

public class RepositorioMesaEmArquivo : RepositorioBaseEmArquivo<Mesa>, IRepositorioMesa
{
    public RepositorioMesaEmArquivo(ContextoDados contexto) : base(contexto) { }

    protected override List<Mesa> ObterRegistros()
    {
        return contexto.Mesas;
    }

    void IRepositorio<Mesa>.EditarRegistro(Guid idRegistro, Mesa registroEditado)
    {
        throw new NotImplementedException();
    }

    void IRepositorio<Mesa>.ExcluirRegistro(Guid idRegistro)
    {
        throw new NotImplementedException();
    }
}
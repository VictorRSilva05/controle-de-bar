using ControleDeBar.Dominio.Compartilhado;
using ControleDeBar.Dominio.ModuloConta;
using ControleDeBar.Dominio.ModuloMesa;
using ControleDeBar.Infraestrura.Arquivos.Compartilhado;

namespace ControleDeBar.Infraestrura.Arquivos.ModuloMesa;

public class RepositorioMesaEmArquivo : RepositorioBaseEmArquivo<Mesa>, IRepositorioMesa
{
    public RepositorioMesaEmArquivo(ContextoDados contexto) : base(contexto) { }

    public void DesocuparMesa(Mesa mesa)
    {
        throw new NotImplementedException();
    }

    public bool MesaContemVinculos(Guid mesaId, List<Conta> contas)
    {
        throw new NotImplementedException();
    }

    public void OcuparMesa(Mesa mesa)
    {
        throw new NotImplementedException();
    }

    public bool VerificarMesaCheia(Mesa mesa, List<Conta> contasAbertas)
    {
        throw new NotImplementedException();
    }

    protected override List<Mesa> ObterRegistros()
    {
        return contexto.Mesas;
    }

}
using ControleDeBar.Dominio.ModuloConta;
using ControleDeBar.Dominio.ModuloMesa;
using ControleDeBar.Infraestrutura.Orm.Compartilhado;
using Microsoft.Identity.Client;

namespace ControleDeBar.Infraestrutura.Orm.ModuloMesa;

public class RepositorioMesaEmOrm : RepositorioBaseEmOrm<Mesa>, IRepositorioMesa
{
    private readonly ControleDeBarDbContext contexto;

    public RepositorioMesaEmOrm(ControleDeBarDbContext contexto) : base(contexto)
    {
        this.contexto = contexto;
    }
   
    public bool VerificarMesaCheia(Mesa mesa, List<Conta> contasAbertas)
    {
        throw new NotImplementedException();
    }
    public void DesocuparMesa(Mesa mesa)
    {
        throw new NotImplementedException();
    }

    public void OcuparMesa(Mesa mesa)
    {
        throw new NotImplementedException();
    }

    public bool MesaContemVinculos(Guid mesaId, List<Conta> contas)
    {
        throw new NotImplementedException();
    }

}

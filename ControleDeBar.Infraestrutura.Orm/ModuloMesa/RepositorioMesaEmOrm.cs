using ControleDeBar.Dominio.ModuloConta;
using ControleDeBar.Dominio.ModuloMesa;
using ControleDeBar.Infraestrutura.Orm.Compartilhado;
using Microsoft.Identity.Client;

namespace ControleDeBar.Infraestrutura.Orm.ModuloMesa;

public class RepositorioMesaEmOrm : IRepositorioMesa
{
    private readonly ControleDeBarDbContext contexto;

    public RepositorioMesaEmOrm(ControleDeBarDbContext contexto)
    {
        this.contexto = contexto;
    }
    public void CadastrarRegistro(Mesa novoRegistro)
    {
        contexto.Mesas.Add(novoRegistro);           
        
        contexto.SaveChanges(); 
    }

    public void DesocuparMesa(Mesa mesa)
    {
        throw new NotImplementedException();
    }

    public bool EditarRegistro(Guid idRegistro, Mesa registroEditado)
    {
        var registroSelecionado = SelecionarRegistroPorId(idRegistro);

        if (registroSelecionado is null)
            return false;

        registroSelecionado.AtualizarRegistro(registroEditado);

        return true;    
    }

    public bool ExcluirRegistro(Guid idRegistro)
    {
        var registroSelecionado = SelecionarRegistroPorId(idRegistro);

        if (registroSelecionado is null)
            return false;

        contexto.Mesas.Remove(registroSelecionado);

        return true;
    }

    public bool MesaContemVinculos(Guid mesaId, List<Conta> contas)
    {
        throw new NotImplementedException();
    }

    public void OcuparMesa(Mesa mesa)
    {
        throw new NotImplementedException();
    }

    public Mesa? SelecionarRegistroPorId(Guid idRegistro)
    {
        return contexto.Mesas.FirstOrDefault(x => x.Id.Equals(idRegistro));
    }

    public List<Mesa> SelecionarRegistros()
    {
        return contexto.Mesas.ToList();
    }

    public bool VerificarMesaCheia(Mesa mesa, List<Conta> contasAbertas)
    {
        throw new NotImplementedException();
    }
}

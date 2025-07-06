using ControleDeBar.Dominio.ModuloProduto;
using System.Diagnostics.CodeAnalysis;

namespace ControleDeBar.Dominio.ModuloConta;

public class Pedido
{
    public Guid Id { get; set; }
    public Conta Conta { get; set; } = null!;
    public Produto Produto { get; set; } = null!;
    public int Quantidade { get; set; }

    public Pedido() { }
    public Pedido(Produto produto, int quantidadeSolicitada) : this()
    {
        Id = Guid.NewGuid();
        Produto = produto;
        Quantidade = quantidadeSolicitada;
    }

    public decimal CalcularTotalParcial()
    {
        return Produto.Valor * Quantidade;
    }

    public override string ToString()
    {
        return $"{Quantidade}x {Produto.Valor}";
    }
}
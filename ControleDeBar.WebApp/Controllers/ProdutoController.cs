using ControleDeBar.Dominio.ModuloProduto;
using ControleDeBar.Infraestrura.Arquivos.ModuloMesa;
using ControleDeBar.Infraestrutura.Orm.Compartilhado;
using ControleDeBar.WebApp.Extensions;
using ControleDeBar.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeBar.WebApp.Controllers;

[Route("produtos")]
public class ProdutoController : Controller
{
    private readonly ControleDeBarDbContext contexo;
    private readonly IRepositorioProduto repositorioProduto;

    public ProdutoController(ControleDeBarDbContext contexo, IRepositorioProduto repositorioProduto)
    {
        this.contexo = contexo;
        this.repositorioProduto = repositorioProduto;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var registros = repositorioProduto.SelecionarRegistros();

        var visualizarVM = new VisualizarProdutosViewModel(registros);

        return View(visualizarVM);
    }

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        var cadastrarVM = new CadastrarProdutoViewModel();

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    [ValidateAntiForgeryToken]
    public IActionResult Cadastrar(CadastrarProdutoViewModel cadastrarVM)
    {
        var registros = repositorioProduto.SelecionarRegistros();

        foreach (var item in registros)
        {
            if (item.Nome.Equals(cadastrarVM.Nome))
            {
                ModelState.AddModelError("CadastroUnico", "Já existe um produto registrado com este nome.");
                break;
            }
        }

        if (!ModelState.IsValid)
            return View(cadastrarVM);

        var entidade = cadastrarVM.ParaEntidade();

        var transacao = contexo.Database.BeginTransaction();

        try
        {
            repositorioProduto.CadastrarRegistro(entidade);

            contexo.SaveChanges();
            transacao.Commit();
        }
        catch (Exception)
        {
            transacao.Rollback();
            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("editar/{id:guid}")]
    public IActionResult Editar(Guid id)
    {
        var registroSelecionado = repositorioProduto.SelecionarRegistroPorId(id);

        var editarVM = new EditarProdutoViewModel(
            id,
            registroSelecionado.Nome,
            registroSelecionado.Valor
        );

        return View(editarVM);
    }

    [HttpPost("editar/{id:guid}")]
    [ValidateAntiForgeryToken]
    public IActionResult Editar(Guid id, EditarProdutoViewModel editarVM)
    {
        var registros = repositorioProduto.SelecionarRegistros();

        foreach (var item in registros)
        {
            if (!item.Id.Equals(id) && item.Nome.Equals(editarVM.Nome))
            {
                ModelState.AddModelError("CadastroUnico", "Já existe um produto registrado com este nome.");
                break;
            }
        }

        if (!ModelState.IsValid)
            return View(editarVM);

        var entidadeEditada = editarVM.ParaEntidade();

        var transacao = contexo.Database.BeginTransaction();

        try
        {
            repositorioProduto.EditarRegistro(id, entidadeEditada);

            contexo.SaveChanges();
            transacao.Commit();
        }
        catch (Exception)
        {
            transacao.Rollback();
            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("excluir/{id:guid}")]
    public IActionResult Excluir(Guid id)
    {
        var registroSelecionado = repositorioProduto.SelecionarRegistroPorId(id);

        var excluirVM = new ExcluirProdutoViewModel(registroSelecionado.Id, registroSelecionado.Nome);

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:guid}")]
    public IActionResult ExcluirConfirmado(Guid id)
    {
        var transacao = contexo.Database.BeginTransaction();

        try
        {
            repositorioProduto.ExcluirRegistro(id);

            contexo.SaveChanges();
            transacao.Commit();
        }
        catch (Exception)
        {
            transacao.Rollback();
            throw;
        }

        return RedirectToAction(nameof(Index));
    }
}
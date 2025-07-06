using ControleDeBar.Dominio.ModuloConta;
using ControleDeBar.Dominio.ModuloMesa;
using ControleDeBar.Dominio.ModuloProduto;
using ControleDeBar.Infraestrutura.SqlServer.ModuloGarcom;
using ControleDeBar.Infraestrutura.SqlServer.ModuloProduto;
using ControleDeBar.Infraestrutura.SQLServer.ModuloConta;
using ControleDeBar.Infraestrutura.SQLServer.ModuloMesa;

namespace ControleDeBar.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IRepositorioGarcom, RepositorioGarcomEmSql>();
            builder.Services.AddScoped<IRepositorioMesa, RepositorioMesaSQL>();
            builder.Services.AddScoped<IRepositorioProduto, RepositorioProdutoEmSql>();
            builder.Services.AddScoped<IRepositorioConta, RepositorioContaSQL>();

            var app = builder.Build();

            app.UseStaticFiles();
            app.UseRouting();
            app.MapDefaultControllerRoute();

            app.Run();
        }
    }
}

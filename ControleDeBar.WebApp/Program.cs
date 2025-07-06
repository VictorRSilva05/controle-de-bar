using ControleDeBar.Dominio.ModuloConta;
using ControleDeBar.Dominio.ModuloMesa;
using ControleDeBar.Dominio.ModuloProduto;
using ControleDeBar.Infraestrutura.SqlServer.ModuloConta;
using ControleDeBar.Infraestrutura.SqlServer.ModuloGarcom;
using ControleDeBar.Infraestrutura.SqlServer.ModuloMesa;
using ControleDeBar.Infraestrutura.SqlServer.ModuloProduto;

namespace ControleDeBar.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IRepositorioGarcom, RepositorioGarcomEmSql>();
            builder.Services.AddScoped<IRepositorioMesa, RepositorioMesaEmSql>();
            builder.Services.AddScoped<IRepositorioProduto, RepositorioProdutoEmSql>();
            builder.Services.AddScoped<IRepositorioConta, RepositorioContaEmSql>();

            var app = builder.Build();

            app.UseStaticFiles();
            app.UseRouting();
            app.MapDefaultControllerRoute();

            app.Run();
        }
    }
}

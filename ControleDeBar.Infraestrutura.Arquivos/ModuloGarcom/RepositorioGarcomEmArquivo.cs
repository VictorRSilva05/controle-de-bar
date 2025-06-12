using ControleDeBar.Dominio.Compartilhado;
using ControleDeBar.Dominio.ModuloGarcom;
using ControleDeBar.Infraestrura.Arquivos.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeBar.Infraestrutura.Arquivos.ModuloGarcom
{
    public class RepositorioGarcomEmArquivo : RepositorioBaseEmArquivo<Garcom>, IRepositorioGarcom
    {
        public RepositorioGarcomEmArquivo(ContextoDados contextoDados) : base(contextoDados)
        {
        }

        protected override List<Garcom> ObterRegistros()
        {
            return contexto.Garcons;
        }

        void IRepositorio<Garcom>.EditarRegistro(Guid idRegistro, Garcom registroEditado)
        {
            throw new NotImplementedException();
        }

        void IRepositorio<Garcom>.ExcluirRegistro(Guid idRegistro)
        {
            throw new NotImplementedException();
        }
    }
}

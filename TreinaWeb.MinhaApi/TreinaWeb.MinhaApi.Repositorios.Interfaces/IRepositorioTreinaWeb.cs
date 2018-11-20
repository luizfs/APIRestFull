using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TreinaWeb.MinhaApi.Repositorios.Interfaces
{
    public interface IRepositorioTreinaWeb<TDominio, Tchave>
        where TDominio: class
    {
        List<TDominio> Selecionar(Expression<Func<TDominio, bool>> where = null);
        TDominio SelecionarPorId(Tchave id);
        void Inserir(TDominio dominio);
        void Atualizar(TDominio dominio);
        void Excluir(TDominio dominio);
        void ExcluirPorId(Tchave id);
    }
}

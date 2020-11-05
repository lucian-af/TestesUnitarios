using ContextoPagamento.Dominio.Entidades;
using System;
using System.Linq.Expressions;

namespace ContextoPagamento.Dominio.Consultas
{
    public static class ProdutoConsulta
    {
        public static Expression<Func<Produto, bool>> PesquisarProdutoAtivo()
        {
            return produto => produto.Ativo;
        }

        public static Expression<Func<Produto, bool>> PesquisarProdutoInativo()
        {
            return produto => produto.Ativo == false;
        }
    }
}

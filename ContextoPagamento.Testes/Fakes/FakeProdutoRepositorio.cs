using ContextoPagamento.Dominio.Entidades;
using ContextoPagamento.Dominio.Repositorio.Interfaces;
using System;
using System.Collections.Generic;

namespace ContextoPagamento.Testes.Fakes
{
    public class FakeProdutoRepositorio : IProdutoRepositorio
    {
        public IEnumerable<Produto> Obter(IEnumerable<Guid> ids)
        {
            IList<Produto> produtos = new List<Produto>();
            produtos.Add(new Produto("Teste 1", 10, true));
            produtos.Add(new Produto("Teste 2", 10, true));
            produtos.Add(new Produto("Teste 3", 10, true));
            produtos.Add(new Produto("Teste 4", 10, false));
            produtos.Add(new Produto("Teste 5", 10, false));

            return produtos;
        }
    }
}

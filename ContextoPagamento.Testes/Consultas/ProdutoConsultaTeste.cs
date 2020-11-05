using ContextoPagamento.Dominio.Consultas;
using ContextoPagamento.Dominio.Entidades;
using ContextoPagamento.Testes.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace ContextoPagamento.Testes.Consultas
{
    [TestClass]
    public class ProdutoConsultaTeste
    {
        private readonly FakeProdutoRepositorio repositorio = new FakeProdutoRepositorio();
        private IList<Produto> _produtos;

        public ProdutoConsultaTeste()
        {
            _produtos = new List<Produto>();
            _produtos.Add(new Produto("Teste 1", 10, true));
            _produtos.Add(new Produto("Teste 2", 10, true));
            _produtos.Add(new Produto("Teste 3", 10, true));
            _produtos.Add(new Produto("Teste 4", 10, false));
            _produtos.Add(new Produto("Teste 5", 10, false));
        }

        [TestMethod]
        [TestCategory("Consulta")]
        public void DadoAConsultaDeProdutosAtivosDeveRetornar3()
        {
            var resultado = _produtos.AsQueryable().Where(ProdutoConsulta.PesquisarProdutoAtivo()).Count();
            Assert.AreEqual(resultado, 3);
        }

        [TestMethod]
        [TestCategory("Consulta")]
        public void DadoAConsultaDeProdutosInativosDeveRetornar2()
        {
            var resultado = _produtos.AsQueryable().Where(ProdutoConsulta.PesquisarProdutoInativo()).Count();
            Assert.AreEqual(resultado, 2);
        }
    }
}

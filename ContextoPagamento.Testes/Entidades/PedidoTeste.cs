using ContextoPagamento.Dominio.Entidades;
using ContextoPagamento.Dominio.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContextoPagamento.Testes.Entidades
{
    [TestClass]
    public class PedidoTeste
    {
        private readonly Cliente cliente = new Cliente("Lucian", "teste@teste.com");
        private readonly Produto produto = new Produto("Teste", 10, true);
        private readonly Desconto desconto = new Desconto(10, DateTime.Now.AddHours(2));

        [TestMethod]
        [TestCategory("Dominio")]
        public void DadoUmPedidoValidoEleDeveGerarUmNumeroCom8Caracteres()
        {
            var pedido = new Pedido(cliente, 0, desconto);
            Assert.AreEqual(8, pedido.Numero.Length);
        }

        [TestMethod]
        [TestCategory("Dominio")]
        public void DadoUmPedidoValidoSuaSituacaoDeveSerAguardandoPagamento()
        {
            var pedido = new Pedido(cliente, 0, new Desconto(0, DateTime.Now));
            Assert.AreEqual(ePedidoStatus.AguardandoPagamento, pedido.Situacao);
        }

        [TestMethod]
        [TestCategory("Dominio")]
        public void DadoUmPagamentoDoPedidoSeuStatusDeveSerAgurdandoEntrega()
        {
            var pedido = new Pedido(cliente, 0, new Desconto(0, DateTime.Now));
            pedido.AdicionarProduto(produto, 1);
            pedido.Pagar(10);
            Assert.AreEqual(ePedidoStatus.AguardandoEntrega, pedido.Situacao);
        }

        [TestMethod]
        [TestCategory("Dominio")]
        public void DadoUmPedidoCanceladoSuaSituacaoDeveSerCancelado()
        {
            var pedido = new Pedido(cliente, 0, new Desconto(0, DateTime.Now));
            pedido.Cancelar();
            Assert.AreEqual(ePedidoStatus.Cancelado, pedido.Situacao);
        }

        [TestMethod]
        [TestCategory("Dominio")]
        public void DadoUmNovoItemSemProdutoOMesmoNaoDeveSerAdicionado()
        {
            var pedido = new Pedido(cliente, 0, new Desconto(0, DateTime.Now));
            pedido.AdicionarProduto(null, 1);
            Assert.AreEqual(0, pedido.Itens.Count);
        }

        [TestMethod]
        [TestCategory("Dominio")]
        public void DadoUmItemComQuantidadeZeroOuMenorOMesmoNaoDeveSerAdicionado()
        {
            var pedido = new Pedido(cliente, 0, new Desconto(0, DateTime.Now));
            pedido.AdicionarProduto(produto, 0);
            Assert.AreEqual(0, pedido.Itens.Count);
        }

        [TestMethod]
        [TestCategory("Dominio")]
        public void DadoUmNovoPedidoValidoSeuTotalDeveSer50()
        {
            var pedido = new Pedido(cliente, 0, new Desconto(0, DateTime.Now));
            pedido.AdicionarProduto(produto, 5);
            Assert.AreEqual(50, pedido.PedidoTotal());
        }

        [TestMethod]
        [TestCategory("Dominio")]
        public void DadoUmDescontoExpiradoOValorDeveSer60()
        {
            var pedido = new Pedido(cliente, 10, new Desconto(10, DateTime.Now.AddHours(-1)));
            pedido.AdicionarProduto(produto, 5);
            Assert.AreEqual(60, pedido.PedidoTotal());
        }

        [TestMethod]
        [TestCategory("Dominio")]
        public void DadoUmDescontoInvalidoOValorDoPedidoDeveSer60()
        {
            var pedido = new Pedido(cliente, 10, null);
            pedido.AdicionarProduto(produto, 5);
            Assert.AreEqual(60, pedido.PedidoTotal());
        }

        [TestMethod]
        [TestCategory("Dominio")]
        public void DadoUmDescontoDe50OValorDoPedidoDeveSer50()
        {
            var pedido = new Pedido(cliente, 0, desconto);
            pedido.AdicionarProduto(produto, 6);
            Assert.AreEqual(50, pedido.PedidoTotal());
        }

        [TestMethod]
        [TestCategory("Dominio")]
        public void DadoUmaTaxaDeEntregaDe10OValorDoPedidoDeveServ60()
        {
            var pedido = new Pedido(cliente, 10, new Desconto(0, DateTime.Now));
            pedido.AdicionarProduto(produto, 5);
            Assert.AreEqual(60, pedido.PedidoTotal());
        }

        [TestMethod]
        [TestCategory("Dominio")]
        public void DadoUmPedidoSemClienteOMesmoDeveSerInvallido()
        {
            var pedido = new Pedido(null, 10, new Desconto(0, DateTime.Now));
            Assert.IsFalse(pedido.Valid);
        }
    }
}

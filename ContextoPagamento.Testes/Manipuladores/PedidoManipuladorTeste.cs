using ContextoPagamento.Dominio.Comandos;
using ContextoPagamento.Dominio.Manipuladores;
using ContextoPagamento.Dominio.Repositorio.Interfaces;
using ContextoPagamento.Testes.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContextoPagamento.Testes.Manipuladores
{
    [TestClass]
    public class PedidoManipuladorTeste
    {
        private readonly FakeClienteRepositorio _clienteRepositorio;
        private readonly FakeTaxaEntregaRepositorio _taxaEntregaRepositorio;
        private readonly FakeDescontoRepositorio _descontoRepositorio;
        private readonly FakeProdutoRepositorio _produtoRepositorio;
        private readonly FakePedidoRepositorio _pedidoRepositorio;
        private readonly ManipuladorPedido _manipulador;

        public PedidoManipuladorTeste()
        {
            _clienteRepositorio = new FakeClienteRepositorio();
            _taxaEntregaRepositorio = new FakeTaxaEntregaRepositorio();
            _descontoRepositorio = new FakeDescontoRepositorio();
            _produtoRepositorio = new FakeProdutoRepositorio();
            _pedidoRepositorio = new FakePedidoRepositorio();

            _manipulador = new ManipuladorPedido(
               _clienteRepositorio,
               _taxaEntregaRepositorio,
               _descontoRepositorio,
               _produtoRepositorio,
               _pedidoRepositorio);
        }

        [TestMethod]
        [TestCategory("Manipulador")]
        public void DadoUmClienteInexistenteOPedidoNaoDeveSerGerado()
        {
            var comando = new ComandoCriarPedido();
            comando.Cliente = null;
            comando.Cep = "17600090";
            comando.CodigoPromocao = "12345678";
            comando.Itens.Add(new ComandoCriarPedidoItem(Guid.NewGuid(), 1));
            comando.Itens.Add(new ComandoCriarPedidoItem(Guid.NewGuid(), 1));
            comando.Validar();

            Assert.AreEqual(comando.Valid, false);
        }

        [TestMethod]
        [TestCategory("Manipulador")]
        public void DadoUmCepInvalidoOPedidoNaoDeveSerGeradoNormalmente()
        {
            var comando = new ComandoCriarPedido();
            comando.Cliente = "12345678911";
            comando.Cep = "17600";
            comando.CodigoPromocao = "12345678";
            comando.Itens.Add(new ComandoCriarPedidoItem(Guid.NewGuid(), 1));
            comando.Itens.Add(new ComandoCriarPedidoItem(Guid.NewGuid(), 1));
            comando.Validar();

            Assert.AreEqual(comando.Valid, false);            
        }

        [TestMethod]
        [TestCategory("Manipulador")]
        public void DadoUmCodigoPromocaoInexistenteOPedidoDeveSerGeradoNormalmente()
        {
            var comando = new ComandoCriarPedido();
            comando.Cliente = "12345678911";
            comando.Cep = "17600090";
            comando.CodigoPromocao = null;
            comando.Itens.Add(new ComandoCriarPedidoItem(Guid.NewGuid(), 1));
            comando.Itens.Add(new ComandoCriarPedidoItem(Guid.NewGuid(), 1));
            comando.Validar();

            _manipulador.Manipulador(comando);

            Assert.AreEqual(comando.Valid, true);
            Assert.AreEqual(_manipulador.Valid, true);
        }

        [TestMethod]
        [TestCategory("Manipulador")]
        public void DadoUmPedidoSemItensOMesmoNaoDeveSerGerado()
        {
            var comando = new ComandoCriarPedido();
            comando.Cliente = "12345678911";
            comando.Cep = "17600090";
            comando.CodigoPromocao = "123456";
            //comando.Itens.Add(new ComandoCriarPedidoItem(Guid.NewGuid(), 1));
            //comando.Itens.Add(new ComandoCriarPedidoItem(Guid.NewGuid(), 1));
            comando.Validar();            

            Assert.AreEqual(comando.Valid, false);
        }

        [TestMethod]
        [TestCategory("Manipulador")]
        public void DadoUmComandoInvalidoOPedidoNaoDeveSerGerado()
        {
            var comando = new ComandoCriarPedido();
            comando.Cliente = ""; // comando invalido
            comando.Cep = "17600090";
            comando.CodigoPromocao = "123456";
            comando.Itens.Add(new ComandoCriarPedidoItem(Guid.NewGuid(), 1));
            comando.Itens.Add(new ComandoCriarPedidoItem(Guid.NewGuid(), 1));
            comando.Validar();                       

            Assert.AreEqual(comando.Valid, false);
        }

        [TestMethod]
        [TestCategory("Manipulador")]
        public void DadoUmComandoValidoOPedidoDeveSerGerado()
        {
            var comando = new ComandoCriarPedido();
            comando.Cliente = "12345678";
            comando.Cep = "17600090";
            comando.CodigoPromocao = "123456";
            comando.Itens.Add(new ComandoCriarPedidoItem(Guid.NewGuid(), 1));
            comando.Itens.Add(new ComandoCriarPedidoItem(Guid.NewGuid(), 1));
            comando.Validar();

            _manipulador.Manipulador(comando);

            Assert.AreEqual(_manipulador.Valid, true);
        }
    }
}

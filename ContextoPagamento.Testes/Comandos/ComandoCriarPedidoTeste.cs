using ContextoPagamento.Dominio.Comandos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ContextoPagamento.Testes.Comandos
{
    [TestClass]
    public class ComandoCriarPedidoTeste
    {
        [TestMethod]
        [TestCategory("Manipulador")]
        public void DadoUmComandoInvalidoOPedidoNaoDeveSerGerado()
        {
            var comando = new ComandoCriarPedido();
            comando.Cliente = "";
            comando.Cep = "17600090";
            comando.CodigoPromocao = "123456";
            comando.Itens.Add(new ComandoCriarPedidoItem(Guid.NewGuid(), 1));
            comando.Itens.Add(new ComandoCriarPedidoItem(Guid.NewGuid(), 1));
            comando.Validar();

            Assert.AreEqual(comando.Valid, false);
        }
    }
}

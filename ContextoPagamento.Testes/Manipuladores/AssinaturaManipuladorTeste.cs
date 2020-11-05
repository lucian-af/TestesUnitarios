using ContextoPagamento.Dominio.Comandos;
using ContextoPagamento.Dominio.Enums;
using ContextoPagamento.Dominio.Manipuladores;
using ContextoPagamento.Testes.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ContextoPagamento.Testes.Manipuladores
{
    [TestClass]
    public class AssinaturaManipuladorTeste
    {
        [TestMethod]
        public void RetornarErroQuandoDocumentoExistir()
        {
            var manipulador = new ManipuladorAssinatura(new FakeAlunoRepositorio(), new FakeEmailService());

            var comando = new ComandoCriarAssinaturaBoleto();
            comando.PrimeiroNome = "LUCIAN";
            comando.Sobrenome = "ALVES FERREIRA";
            comando.Documento = "99999999999";
            comando.Email = "hello@teste.com2";
            comando.CodigoBarras = "123456789";
            comando.NossoNumero = "123456987";
            comando.PaymentNumber = "123121";
            comando.DataPagamento = DateTime.Now;
            comando.DataExpiracao = DateTime.Now.AddMonths(1);
            comando.ValorTotal = 60;
            comando.ValorPago = 60;
            comando.Pagador = "JOSE LTDA";
            comando.PagadorDocumento = "12345678911";
            comando.PagadorTipoDocumento = eTipoDocumento.CPF;
            comando.PagadorEmail = "joseltda@test.com";
            comando.Descricao = "teste";
            comando.Numero = "1";
            comando.Bairro = "teste do teste";
            comando.Cidade = "TesteCity";
            comando.Estado = "TesteState";
            comando.Pais = "Teste";
            comando.Cep = "123000010";

            manipulador.Manipulador(comando);
            Assert.AreEqual(false, manipulador.Valid);
        }
    }
}

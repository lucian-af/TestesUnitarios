using ContextoPagamento.Dominio.Enums;
using ContextoPagamento.Dominio.ValorObjetos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContextoPagamento.Testes.ValorObjetos
{
    // Red, Green, Refactor
    // 1º Deixa tudo falhar;
    // 2º Deixa tudo funcionar;
    // 3º Refatore
    [TestClass]
    public class DocumentoTeste
    {
        [TestMethod]
        public void RetornarErroQuandoCPFInvalido()
        {
            var documento = new Documento("123", eTipoDocumento.CPF);
            Assert.IsTrue(documento.Invalid);
        }

        [TestMethod]
        public void RetornarErroQuandoCNPJInvalido()
        {
            var documento = new Documento("123/1", eTipoDocumento.CNPJ);
            Assert.IsTrue(documento.Invalid);
        }

        [TestMethod]
        // [DataRow] - com esse anotation conseguimos testar mais de um CPF no método
        // mudamos o metodo para receber um parametro para funcionar
        public void RetornarSucessoQuandoCPFValido()
        {
            var documento = new Documento("346.640.438-05", eTipoDocumento.CPF);
            Assert.IsTrue(documento.Valid);
        }

        [TestMethod]
        public void RetornarSucessoQuandoCNPJValido()
        {
            var documento = new Documento("01.576.122/0001-93", eTipoDocumento.CNPJ);
            Assert.IsTrue(documento.Valid);
        }
    }
}

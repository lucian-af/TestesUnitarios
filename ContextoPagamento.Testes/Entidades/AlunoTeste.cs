using ContextoPagamento.Dominio.Entidades;
using ContextoPagamento.Dominio.Enums;
using ContextoPagamento.Dominio.ValorObjetos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContextoPagamento.Testes.Entidades
{
    [TestClass]
    public class AlunoTeste
    {
        private readonly Aluno _aluno;        
        private readonly Nome _nome;
        private readonly Email _email;
        private readonly Documento _documento;
        private readonly Endereco _endereco;

        public AlunoTeste()
        {
            _documento = new Documento("346.640.438.05", eTipoDocumento.CPF);
            _email = new Email("lucian@hotmail.com");
            _nome = new Nome("LUCIAN", "ALVES FERREIRA");
            _aluno = new Aluno(_nome, _documento, _email);
            _endereco = new Endereco("RUA VISTA ALEGRE", "195", "JD. ARITANA", "TUPÃ", "SP", "BRASIL", "17602700");
            
        }

        [TestMethod]
        public void RetornarErroQuandoTiverAssinaturaAtiva()
        {
            var assinatura = new Assinatura(null);
            var pagamento = new PagamentoPayPal(
                "123456",
                DateTime.Now,
                DateTime.Now.AddDays(5),
                100,
                100,
                "Lucian AF",
                _documento,
                _endereco,
                _email);

            assinatura.AdicionarPagamento(pagamento);
            _aluno.AdicionarAssinatura(assinatura);
            _aluno.AdicionarAssinatura(assinatura);
            Assert.IsTrue(_aluno.Invalid);
        }

        [TestMethod]
        public void RetornarErroQuandoAssinaturaSemPagamento()
        {
            var assinatura = new Assinatura(null);
            _aluno.AdicionarAssinatura(assinatura);
            Assert.IsTrue(_aluno.Invalid);
        }

        [TestMethod]
        public void RetornarSucessoQuandoAdicionarAssinatura()
        {
            var assinatura = new Assinatura(null);
            var pagamento = new PagamentoPayPal(
                "123456",
                DateTime.Now,
                DateTime.Now.AddDays(5),
                100,
                100,
                "Lucian AF",
                _documento,
                _endereco,
                _email);

            assinatura.AdicionarPagamento(pagamento);
            _aluno.AdicionarAssinatura(assinatura);
            Assert.IsTrue(_aluno.Valid);
        }
    }
}

using ContextoPagamento.Dominio.ValorObjetos;
using System;

namespace ContextoPagamento.Dominio.Entidades
{
    public class PagamentoBoleto : Pagamento
    {
        public PagamentoBoleto(
            string codigoBarras,
            string nossoNumero,
            DateTime dataPagamento,
            DateTime dataExpiracao,
            decimal valorTotal,
            decimal valorPago,
            string pagador,
            Documento documento,
            Endereco endereco,
            Email email) : base(
                dataPagamento,
                dataExpiracao,
                valorTotal,
                valorPago,
                pagador,
                documento,
                endereco,
                email)
        {
            CodigoBarras = codigoBarras;
            NossoNumero = nossoNumero;
        }

        public string CodigoBarras { get; private set; }
        public string NossoNumero { get; private set; }
    }
}

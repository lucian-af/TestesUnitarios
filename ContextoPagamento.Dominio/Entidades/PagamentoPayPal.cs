using ContextoPagamento.Dominio.ValorObjetos;
using System;

namespace ContextoPagamento.Dominio.Entidades
{
    public class PagamentoPayPal : Pagamento
    {
        public PagamentoPayPal(
            string codigoTransacao,
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
            CodigoTransacao = codigoTransacao;
        }

        public string CodigoTransacao { get; private set; }
    }
}

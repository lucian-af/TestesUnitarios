using ContextoPagamento.Dominio.ValorObjetos;
using System;

namespace ContextoPagamento.Dominio.Entidades
{
    public class PagamentoCartao : Pagamento
    {
        public PagamentoCartao(
            string nome,
            string numeroCartao,
            string ultimaTransacao,
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
            Nome = nome;
            NumeroCartao = numeroCartao;
            UltimaTransacao = ultimaTransacao;
        }

        public string Nome { get; private set; }
        public string NumeroCartao { get; private set; }
        public string UltimaTransacao { get; private set; }
    }
}

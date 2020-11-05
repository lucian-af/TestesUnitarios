using ContextoPagamento.Compartilhado.Entidades;
using ContextoPagamento.Dominio.ValorObjetos;
using Flunt.Validations;
using System;

namespace ContextoPagamento.Dominio.Entidades
{
    public abstract class Pagamento : Entidade
    {
        protected Pagamento(DateTime dataPagamento, DateTime dataExpiracao, decimal valorTotal, decimal valorPago,
            string pagador, Documento documento, Endereco endereco, Email email)
        {
            Numero = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10).ToUpper();
            DataPagamento = dataPagamento;
            DataExpiracao = dataExpiracao;
            ValorTotal = valorTotal;
            ValorPago = valorPago;
            Pagador = pagador;
            Documento = documento;
            Endereco = endereco;
            Email = email;

            AddNotifications(new Contract()
                .Requires()
                .IsLowerOrEqualsThan(0, ValorTotal, "Pagamento.ValorTotal", "Valor Total inválido!")
                .IsGreaterOrEqualsThan(ValorTotal, ValorPago, "Pagamento.ValorPago", "O Valor Pago é menor que o Valor Total!"));
        }

        #region Atributos
        public string Numero { get; private set; }
        public DateTime DataPagamento { get; private set; }
        public DateTime DataExpiracao { get; private set; }
        public decimal ValorTotal { get; private set; }
        public decimal ValorPago { get; private set; }
        public string Pagador { get; private set; }
        public Documento Documento { get; private set; }
        public Endereco Endereco { get; private set; }
        public Email Email { get; private set; } 
        #endregion
    }
}

using ContextoPagamento.Compartilhado.Entidades;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContextoPagamento.Dominio.Entidades
{
    public class Desconto : Entidade
    {
        public Desconto(decimal valor, DateTime dataExpiracao)
        {
            Valor = valor;
            DataExpiracao = dataExpiracao;

            AddNotifications(new Contract()
                .Requires()                
                .IsTrue(CupomValido(), "Desconto.DataExpiracao", "Desconto expirado"));
        }

        public decimal Valor { get; private set; }
        public DateTime DataExpiracao { get; private set; }

        public bool CupomValido()
        {
            return DateTime.Compare(DateTime.Now, DataExpiracao) < 0;
        }
        public decimal DescontoValor()
        {
            if (CupomValido())
                return Valor;

            return 0;
        }
    }
}

using ContextoPagamento.Compartilhado.Entidades;
using Flunt.Validations;
using System;
using System.Collections.Generic;

namespace ContextoPagamento.Dominio.Entidades
{
    public class Assinatura : Entidade
    {
        private List<Pagamento> _pagamentos;

        public Assinatura(DateTime? dataExpiracao)
        {
            DataCriacao = DateTime.Now;
            DataUltimaAtualizacao = DateTime.Now;
            DataExpiracao = dataExpiracao;
            Ativo = true;
            _pagamentos = new List<Pagamento>();
        }

        #region Atributos
        public DateTime DataCriacao { get; private set; }
        public DateTime DataUltimaAtualizacao { get; private set; }
        public DateTime? DataExpiracao { get; private set; }
        public bool Ativo { get; private set; }
        public IReadOnlyCollection<Pagamento> Pagamentos { get { return _pagamentos.ToArray(); } }
        #endregion

        #region Metodos
        public void Ativar()
        {
            Ativo = true;
            DataUltimaAtualizacao = DateTime.Now;
        }
        public void Desativar()
        {
            Ativo = false;
            DataUltimaAtualizacao = DateTime.Now;
        }
        public void AdicionarPagamento(Pagamento pagamento)
        {
            AddNotifications(new Contract()
                .Requires()
                .IsGreaterThan(DateTime.Now, pagamento.DataPagamento, "Assinatura.Pagamentos", "Data do pagamento inválida!"));

            _pagamentos.Add(pagamento);
        }
        #endregion
    }
}
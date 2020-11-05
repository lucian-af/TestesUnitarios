using ContextoPagamento.Compartilhado.Entidades;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContextoPagamento.Dominio.Entidades
{
    public class PedidoItem : Entidade
    {
        public PedidoItem(Produto produto, int quantidade)
        {
            Produto = produto;
            Preco = Produto != null ? Produto.Preco : 0;
            Quantidade = quantidade;

            AddNotifications(new Contract()
                .Requires()
                .IsNotNull(Produto, "PedidoItem.Produto", "Produto não encontrado")
                .IsGreaterThan(Quantidade, 0, "PedidoItem.Quantidade", "Quantidade deve ser maior que zero"));
        }

        public Produto Produto { get; private set; }
        public decimal Preco { get; private set; }
        public int Quantidade { get; private set; }

        public decimal SubTotal()
        {
            return Quantidade * Preco;
        }
    }
}

using ContextoPagamento.Compartilhado.Entidades;
using ContextoPagamento.Dominio.Enums;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;

namespace ContextoPagamento.Dominio.Entidades
{
    public class Pedido : Entidade
    {
        public Pedido(Cliente cliente, decimal taxaEntrega, Desconto desconto)
        {
            Cliente = cliente;
            Data = DateTime.Now;
            Numero = Guid.NewGuid().ToString().Substring(0, 8);
            Situacao = ePedidoStatus.AguardandoPagamento;
            TaxaEntrega = taxaEntrega;
            Desconto = desconto;
            Itens = new List<PedidoItem>();

            AddNotifications(new Contract()
                .Requires()
                .IsNotNull(Cliente, "Pedido.Cliente", "Cliente não encontrado!")
                .IsLowerOrEqualsThan(0, Itens.Count, "Pedido.Itens", "Informe ao menos um item do pedido"));
        }

        public Cliente Cliente { get; private set; }
        public DateTime Data { get; private set; }
        public string Numero { get; private set; }
        public IList<PedidoItem> Itens { get; private set; }
        public ePedidoStatus Situacao { get; private set; }
        public decimal TaxaEntrega { get; private set; }
        public Desconto Desconto { get; private set; }

        public void AdicionarProduto(Produto produto, int quantidade)
        {
            var item = new PedidoItem(produto, quantidade);
            if (item.Valid)
                Itens.Add(item);
        }
        public decimal PedidoTotal()
        {
            decimal total = 0;
            foreach (var item in Itens)
            {
                total += item.SubTotal();
            }

            total += TaxaEntrega;
            total -= Desconto != null ? Desconto.DescontoValor() : 0;

            return total;
        }
        public void Pagar(decimal valor)
        {
            if (valor == PedidoTotal())
                Situacao = ePedidoStatus.AguardandoEntrega;
        }
        public void Cancelar()
        {
            Situacao = ePedidoStatus.Cancelado;
        }
    }
}

using ContextoPagamento.Compartilhado.Comandos;
using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace ContextoPagamento.Dominio.Comandos
{
    public class ComandoCriarPedidoItem : Notifiable, IComando
    {
        public ComandoCriarPedidoItem()
        {
        }

        public ComandoCriarPedidoItem(Guid produto, int quantidade)
        {
            Produto = produto;
            Quantidade = quantidade;
        }

        public Guid Produto { get; set; }
        public int Quantidade { get; set; }

        public void Validar()
        {
            AddNotifications(new Contract()
                .Requires()
                .HasLen(Produto.ToString(), 32, "ComandoCriarPedidoItem.Produto", "Produto inválido")
                .IsGreaterThan(Quantidade, 0, "ComandoCriarPedidoItem.Quantidade", "Quantidade inválida"));
        }
    }
}

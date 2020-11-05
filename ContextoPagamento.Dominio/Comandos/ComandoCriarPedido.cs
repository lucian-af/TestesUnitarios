using ContextoPagamento.Compartilhado.Comandos;
using Flunt.Notifications;
using Flunt.Validations;
using System.Collections.Generic;

namespace ContextoPagamento.Dominio.Comandos
{
    public class ComandoCriarPedido : Notifiable, IComando
    {
        public ComandoCriarPedido()
        {
            Itens = new List<ComandoCriarPedidoItem>();
        }

        public ComandoCriarPedido(string cliente, string cep, string codigoPromocao, IList<ComandoCriarPedidoItem> itens)
        {
            Cliente = cliente;
            Cep = cep;
            CodigoPromocao = codigoPromocao;
            Itens = itens;
        }

        public string Cliente { get; set; }
        public string Cep { get; set; }
        public string CodigoPromocao { get; set; }
        public IList<ComandoCriarPedidoItem> Itens { get; set; }

        public void Validar()
        {
            AddNotifications(new Contract()
                .Requires()
                .HasLen(Cliente, 11, "ComandoCriarPedido.Cliente", "Cliente inválido")
                .HasLen(Cep, 8, "ComandoCriarPedido.Cep", "Cep inválido")
                .IsGreaterThan(Itens.Count, 0, "ComandoCriarPedido.Itens", "Informe ao menos um item"));
        }
    }
}

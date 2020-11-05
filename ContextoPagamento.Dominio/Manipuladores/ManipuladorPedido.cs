using ContextoPagamento.Compartilhado.Comandos;
using ContextoPagamento.Compartilhado.Manipuladores;
using ContextoPagamento.Dominio.Comandos;
using ContextoPagamento.Dominio.Entidades;
using ContextoPagamento.Dominio.Lib;
using ContextoPagamento.Dominio.Repositorio.Interfaces;
using Flunt.Notifications;
using System;
using System.Linq;

namespace ContextoPagamento.Dominio.Manipuladores
{
    public class ManipuladorPedido : Notifiable,
        IManipulador<ComandoCriarPedido>
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly ITaxaEntregaRepositorio _taxaEntregaRepositorio;
        private readonly IDescontoRepositorio _descontoRepositorio;
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IPedidoRepositorio _pedidoRepositorio;

        public ManipuladorPedido(
            IClienteRepositorio clienteRepositorio,
            ITaxaEntregaRepositorio taxaEntregaRepositorio,
            IDescontoRepositorio descontoRepositorio,
            IProdutoRepositorio produtoRepositorio,
            IPedidoRepositorio pedidoRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
            _taxaEntregaRepositorio = taxaEntregaRepositorio;
            _descontoRepositorio = descontoRepositorio;
            _produtoRepositorio = produtoRepositorio;
            _pedidoRepositorio = pedidoRepositorio;
        }

        public IComandoResultado Manipulador(ComandoCriarPedido comando)
        {
            comando.Validar();

            if (comando.Invalid)
                return new ComandoResultado(false, "Pedido inválido", comando);

            // 1 - Obtem o Cliente
            var cliente = _clienteRepositorio.Obter(comando.Cliente);
            // 2 - Calcula a taxa de entrega
            var taxaEntrega = _taxaEntregaRepositorio.Obter(comando.Cep);
            var desconto = _descontoRepositorio.Obter(comando.CodigoPromocao);

            var produtos = _produtoRepositorio.Obter(ExtrairGuids.Extrair(comando.Itens)).ToList();
            var pedido = new Pedido(cliente, taxaEntrega, desconto);

            foreach (var item in comando.Itens)
            {
                var produto = produtos.Where(pro => pro.Id == item.Produto).FirstOrDefault();
                pedido.AdicionarProduto(produto, item.Quantidade);
            }

            AddNotifications(pedido.Notifications);

            if (Invalid)
                return new ComandoResultado(false, "Falha ao gerar pedido", pedido);

            _pedidoRepositorio.Salvar(pedido);
            return new ComandoResultado(true, $"Pedido Nº {pedido.Numero} gerado com sucesso", pedido);
        }
    }
}

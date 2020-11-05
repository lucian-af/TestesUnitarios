using ContextoPagamento.Dominio.Entidades;

namespace ContextoPagamento.Dominio.Repositorio.Interfaces
{
    public interface IPedidoRepositorio
    {
        void Salvar(Pedido pedido);
    }
}

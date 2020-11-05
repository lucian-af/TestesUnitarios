using ContextoPagamento.Dominio.Entidades;

namespace ContextoPagamento.Dominio.Repositorio.Interfaces
{
    public interface IDescontoRepositorio
    {
        Desconto Obter(string codigo);
    }
}

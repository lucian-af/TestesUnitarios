using ContextoPagamento.Dominio.Entidades;

namespace ContextoPagamento.Dominio.Repositorio.Interfaces
{
    public interface IClienteRepositorio
    {
        Cliente Obter(string documento);
    }
}

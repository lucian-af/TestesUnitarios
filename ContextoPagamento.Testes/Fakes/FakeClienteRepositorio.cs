using ContextoPagamento.Dominio.Entidades;
using ContextoPagamento.Dominio.Repositorio.Interfaces;

namespace ContextoPagamento.Testes.Fakes
{
    public class FakeClienteRepositorio : IClienteRepositorio
    {
        public Cliente Obter(string documento)
        {
            if (documento == "12345678911")
                return new Cliente("Lucian AF", "teste@teste.com");

            return null;
        }
    }
}

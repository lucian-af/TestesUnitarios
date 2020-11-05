using ContextoPagamento.Dominio.Repositorio.Interfaces;

namespace ContextoPagamento.Testes.Fakes
{
    public class FakeTaxaEntregaRepositorio : ITaxaEntregaRepositorio
    {
        public decimal Obter(string cep) => 10;
    }
}

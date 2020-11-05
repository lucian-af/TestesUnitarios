namespace ContextoPagamento.Dominio.Repositorio.Interfaces
{
    public interface ITaxaEntregaRepositorio
    {
        decimal Obter(string cep);
    }
}

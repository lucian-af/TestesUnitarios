using ContextoPagamento.Compartilhado.Comandos;

namespace ContextoPagamento.Compartilhado.Manipuladores
{
    public interface IManipulador<T> where T : IComando
    {
        IComandoResultado Manipulador(T comando);
    }
}

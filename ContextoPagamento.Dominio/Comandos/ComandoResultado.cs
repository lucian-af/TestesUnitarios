using ContextoPagamento.Compartilhado.Comandos;

namespace ContextoPagamento.Dominio.Comandos
{
    /// <summary>
    /// Objetivo desse comando é padronizar nosso retorno ao frontend
    /// </summary>
    public class ComandoResultado : IComandoResultado
    {
        public ComandoResultado()
        {
        }
        public ComandoResultado(bool sucesso, string mensagem, object dado)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Dado = dado;
        }

        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public object Dado { get; set; }
    }
}

using ContextoPagamento.Dominio.Servicos;

namespace ContextoPagamento.Testes.Fakes
{
    public class FakeEmailService : IEmailServico
    {
        public void Enviar(string para, string email, string assunto, string corpoEmail)
        {

        }
    }
}

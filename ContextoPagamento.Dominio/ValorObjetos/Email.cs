using ContextoPagamento.Compartilhado.ValorObjetos;
using Flunt.Validations;

namespace ContextoPagamento.Dominio.ValorObjetos
{
    public class Email : ValorObjeto
    {
        public Email(string endereco)
        {
            Endereco = endereco;

            AddNotifications(new Contract()
                .Requires()
                .IsEmail(Endereco, "Email.Endereco", "E-mail inválido!"));
        }

        public string Endereco { get; private set; }
    }
}

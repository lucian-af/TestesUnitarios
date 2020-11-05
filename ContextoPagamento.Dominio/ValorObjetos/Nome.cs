using ContextoPagamento.Compartilhado.ValorObjetos;
using Flunt.Validations;

namespace ContextoPagamento.Dominio.ValorObjetos
{
    public class Nome : ValorObjeto
    {
        public Nome(string nome, string sobrenome)
        {
            PrimeiroNome = nome;
            Sobrenome = sobrenome;

            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(PrimeiroNome, 3, $"Nome.{nameof(PrimeiroNome)}", "Nome deve conter ao menos 3 caracteres!")
                .HasMinLen(Sobrenome, 3, $"Nome.{nameof(Sobrenome)}", "Sobrenome deve conter ao menos 3 caracteres!")
                .HasMaxLen(PrimeiroNome, 40, $"Nome.{nameof(PrimeiroNome)}", "Nome deve conter no máximo 40 caracteres!"));
        }

        public string PrimeiroNome { get; private set; }
        public string Sobrenome { get; private set; }
    }
}

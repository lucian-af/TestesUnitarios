using ContextoPagamento.Compartilhado.Entidades;
using Flunt.Validations;

namespace ContextoPagamento.Dominio.Entidades
{
    public class Cliente : Entidade
    {
        public Cliente(string nome, string email)
        {
            Nome = nome;
            Email = email;

            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(Nome, 3, $"Cliente.{nameof(Nome)}", "Nome deve conter ao menos 3 caracteres!")
                .HasMaxLen(Nome, 40, $"Cliente.{nameof(Nome)}", "Nome deve conter no máximo 40 caracteres!")
                .IsEmail(Email, "Cliente.Email", "E-mail inválido"));
        }

        public string Nome { get; private set; }
        public string Email { get; private set; }
    }
}

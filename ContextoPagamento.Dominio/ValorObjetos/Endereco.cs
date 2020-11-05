using ContextoPagamento.Compartilhado.ValorObjetos;
using Flunt.Validations;

namespace ContextoPagamento.Dominio.ValorObjetos
{
    public class Endereco : ValorObjeto
    {
        public Endereco(string descricao, string numero, string bairro, string cidade, string estado, string pais, string cep)
        {
            Descricao = descricao;
            Numero = numero;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            Pais = pais;
            Cep = cep;

            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(Descricao, 3, $"Endereco.{nameof(Descricao)}", "Rua/Av. deve conter no mínimo 3 caracteres!")
                .HasMaxLen(Descricao, 100, $"Endereco.{nameof(Descricao)}", "Rua/Av. deve conter no máximo 100 caracteres!")
                .HasMinLen(Numero, 1, $"Endereco.{nameof(Numero)}", "Número inválido!")
                .HasMinLen(Bairro, 3, $"Endereco.{nameof(Bairro)}", "Bairro deve conter no mínimo 3 caracteres!")
                .HasMaxLen(Bairro, 100, $"Endereco.{nameof(Bairro)}", "Bairro deve conter no máximo 100 caracteres!"));
        }

        #region Atributos
        public string Descricao { get; private set; }
        public string Numero { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public string Pais { get; private set; }
        public string Cep { get; private set; } 
        #endregion
    }
}

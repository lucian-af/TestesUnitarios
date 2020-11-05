using ContextoPagamento.Compartilhado.ValorObjetos;
using ContextoPagamento.Dominio.Enums;
using Flunt.Validations;
using Shared = ContextoPagamento.Compartilhado.Lib;

namespace ContextoPagamento.Dominio.ValorObjetos
{
    public class Documento : ValorObjeto
    {
        public Documento(string numero, eTipoDocumento tipo)
        {
            Numero = numero;
            Tipo = tipo;

            AddNotifications(new Contract()
                .Requires()
                .IsTrue(Validar(), $"Documento.{Numero}", "Documento inválido!"));
        }

        #region Atributos
        public string Numero { get; private set; }
        public eTipoDocumento Tipo { get; private set; }
        #endregion
        private bool Validar()
        {
            var retorno = false;

            switch (Tipo)
            {
                case eTipoDocumento.CPF:
                    retorno = Shared.Lib.CpfValido(Numero);
                    break;
                case eTipoDocumento.CNPJ:
                    retorno = Shared.Lib.CnpjValido(Numero);
                    break;
            }

            return retorno;
        }
    }
}

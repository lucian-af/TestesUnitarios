using System;

namespace ContextoPagamento.Compartilhado.Lib
{
    public static class Lib
    {
        public static string LimparCaracterEspecial(string texto)
        {
            var retorno = "";

            try
            {
                retorno = texto.Replace("-", "")
                    .Replace(".", "")
                    .Replace(" ", "")
                    .Replace("/", "")
                    .Replace("\\", "");
            }
            catch
            {
                retorno = "";
            }

            return retorno;
        }
        public static bool CpfValido(string cpf)
        {
            cpf = LimparCaracterEspecial(cpf);

            if (cpf.Length != 11)
                return false;

            #region Valida Cpfs conhecidos
            if (cpf.Equals("00000000000") ||
                cpf.Equals("11111111111") ||
                cpf.Equals("22222222222") ||
                cpf.Equals("33333333333") ||
                cpf.Equals("44444444444") ||
                cpf.Equals("55555555555") ||
                cpf.Equals("66666666666") ||
                cpf.Equals("77777777777") ||
                cpf.Equals("88888888888") ||
                cpf.Equals("99999999999"))
                return false;
            #endregion            

            #region Primeira Validacao
            var PrimeiroDigitoVerificador = Convert.ToInt32(cpf[9].ToString());

            var restoPrimeiraValidacao = (
                  (Convert.ToInt32(cpf[0].ToString()) * 10)
                + (Convert.ToInt32(cpf[1].ToString()) * 9)
                + (Convert.ToInt32(cpf[2].ToString()) * 8)
                + (Convert.ToInt32(cpf[3].ToString()) * 7)
                + (Convert.ToInt32(cpf[4].ToString()) * 6)
                + (Convert.ToInt32(cpf[5].ToString()) * 5)
                + (Convert.ToInt32(cpf[6].ToString()) * 4)
                + (Convert.ToInt32(cpf[7].ToString()) * 3)
                + (Convert.ToInt32(cpf[8].ToString()) * 2)) % 11;

            var resultadoPrimeiraValidacao = restoPrimeiraValidacao == 0 || restoPrimeiraValidacao == 1 ? 0 : 11 - restoPrimeiraValidacao;

            if (resultadoPrimeiraValidacao != PrimeiroDigitoVerificador)
                return false;
            #endregion

            #region Segunda Validacao
            var SegundoDigitoVerificador = Convert.ToInt32(cpf[10].ToString());

            var restoSegundaValidacao = (
                  (Convert.ToInt32(cpf[0].ToString()) * 11)
                + (Convert.ToInt32(cpf[1].ToString()) * 10)
                + (Convert.ToInt32(cpf[2].ToString()) * 9)
                + (Convert.ToInt32(cpf[3].ToString()) * 8)
                + (Convert.ToInt32(cpf[4].ToString()) * 7)
                + (Convert.ToInt32(cpf[5].ToString()) * 6)
                + (Convert.ToInt32(cpf[6].ToString()) * 5)
                + (Convert.ToInt32(cpf[7].ToString()) * 4)
                + (Convert.ToInt32(cpf[8].ToString()) * 3)
                + (Convert.ToInt32(cpf[9].ToString()) * 2)) % 11;

            var resultadoSegundaValidacao = restoSegundaValidacao == 0 || restoSegundaValidacao == 1 ? 0 : 11 - restoSegundaValidacao;

            if (resultadoSegundaValidacao != SegundoDigitoVerificador)
                return false;
            #endregion

            return true;
        }
        public static bool CnpjValido(string cnpj)
        {
            cnpj = Lib.LimparCaracterEspecial(cnpj);
            if (cnpj.Length != 14)
                return false;

            #region Valida Cnpjs conhecidos
            if (cnpj.Equals("00000000000000") ||
                cnpj.Equals("11111111111111") ||
                cnpj.Equals("22222222222222") ||
                cnpj.Equals("33333333333333") ||
                cnpj.Equals("44444444444444") ||
                cnpj.Equals("55555555555555") ||
                cnpj.Equals("66666666666666") ||
                cnpj.Equals("77777777777777") ||
                cnpj.Equals("88888888888888") ||
                cnpj.Equals("99999999999999"))
                return false;
            #endregion            

            #region Primeira Validacao
            var PrimeiroDigitoVerificador = Convert.ToInt32(cnpj[12].ToString());

            var restoPrimeiraValidacao = (
                  (Convert.ToInt32(cnpj[11].ToString()) * 2)
                + (Convert.ToInt32(cnpj[10].ToString()) * 3)
                + (Convert.ToInt32(cnpj[9].ToString()) * 4)
                + (Convert.ToInt32(cnpj[8].ToString()) * 5)
                + (Convert.ToInt32(cnpj[7].ToString()) * 6)
                + (Convert.ToInt32(cnpj[6].ToString()) * 7)
                + (Convert.ToInt32(cnpj[5].ToString()) * 8)
                + (Convert.ToInt32(cnpj[4].ToString()) * 9)
                + (Convert.ToInt32(cnpj[3].ToString()) * 2)
                + (Convert.ToInt32(cnpj[2].ToString()) * 3)
                + (Convert.ToInt32(cnpj[1].ToString()) * 4)
                + (Convert.ToInt32(cnpj[0].ToString()) * 5)) % 11;

            var resultadoPrimeiraValidacao = restoPrimeiraValidacao == 0 || restoPrimeiraValidacao == 1 ? 0 : 11 - restoPrimeiraValidacao;

            if (resultadoPrimeiraValidacao != PrimeiroDigitoVerificador)
                return false;
            #endregion

            #region Segunda Validacao
            var SegundoDigitoVerificador = Convert.ToInt32(cnpj[13].ToString());

            var restoSegundaValidacao = (
                  (Convert.ToInt32(cnpj[12].ToString()) * 2)
                + (Convert.ToInt32(cnpj[11].ToString()) * 3)
                + (Convert.ToInt32(cnpj[10].ToString()) * 4)
                + (Convert.ToInt32(cnpj[9].ToString()) * 5)
                + (Convert.ToInt32(cnpj[8].ToString()) * 6)
                + (Convert.ToInt32(cnpj[7].ToString()) * 7)
                + (Convert.ToInt32(cnpj[6].ToString()) * 8)
                + (Convert.ToInt32(cnpj[5].ToString()) * 9)
                + (Convert.ToInt32(cnpj[4].ToString()) * 2)
                + (Convert.ToInt32(cnpj[3].ToString()) * 3)
                + (Convert.ToInt32(cnpj[2].ToString()) * 4)
                + (Convert.ToInt32(cnpj[1].ToString()) * 5)
                + (Convert.ToInt32(cnpj[0].ToString()) * 6)) % 11;

            var resultadoSegundaValidacao = restoSegundaValidacao == 0 || restoSegundaValidacao == 1 ? 0 : 11 - restoSegundaValidacao;

            if (resultadoSegundaValidacao != SegundoDigitoVerificador)
                return false;
            #endregion

            return true;
        }
    }
}

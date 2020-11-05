using ContextoPagamento.Dominio.Entidades;
using ContextoPagamento.Dominio.Repositorio.Interfaces;
using System;

namespace ContextoPagamento.Testes.Fakes
{
    public class FakeAlunoRepositorio : IAlunoRepositorio
    {
        public void CriarAssinatura(Aluno aluno)
        {
            throw new NotImplementedException();
        }

        public bool DocumentoExiste(string documento)
        {
            if (documento.Equals("99999999999"))
                return true;

            return false;
        }

        public bool EmailExiste(string email)
        {
            if (email.Equals("hello@teste.com"))
                return true;

            return false;
        }
    }
}

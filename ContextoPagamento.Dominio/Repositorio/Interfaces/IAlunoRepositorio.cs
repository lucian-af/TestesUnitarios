using ContextoPagamento.Dominio.Entidades;

namespace ContextoPagamento.Dominio.Repositorio.Interfaces
{
    public interface IAlunoRepositorio
    {
        bool DocumentoExiste(string documento);
        bool EmailExiste(string email);
        void CriarAssinatura(Aluno aluno);
    }
}

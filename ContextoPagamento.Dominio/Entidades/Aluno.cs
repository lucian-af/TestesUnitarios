using ContextoPagamento.Compartilhado.Entidades;
using ContextoPagamento.Dominio.ValorObjetos;
using Flunt.Validations;
using System.Collections.Generic;
using System.Linq;

namespace ContextoPagamento.Dominio.Entidades
{
    public class Aluno : Entidade
    {
        private IList<Assinatura> _assinaturas;

        public Aluno(Nome nome, Documento documento, Email email)
        {
            Nome = nome;
            Email = email;
            Documento = documento;
            _assinaturas = new List<Assinatura>();

            AddNotifications(Nome, Documento, Email);
        }

        #region Atributos
        public Nome Nome { get; private set; }
        public Documento Documento { get; private set; }
        public Email Email { get; private set; }
        public Endereco Endereco { get; private set; }
        public IReadOnlyCollection<Assinatura> Assinaturas { get { return _assinaturas.ToArray(); } }
        #endregion

        #region Metodos
        public void AdicionarAssinatura(Assinatura assinatura)
        {
            var assinaturaAtiva = false;

            foreach (var ass in _assinaturas)
            {
                if (ass.Ativo)
                    assinaturaAtiva = true;
            }

            AddNotifications(new Contract()
                .Requires()
                .IsFalse(assinaturaAtiva, "Aluno.Assinaturas", "Você já tem uma assinatura ativa!")
                .AreNotEquals(0, assinatura.Pagamentos.Count, "Aluno.Assinaturas.Pagamento", "Nenhum pagamento encontrado!"));

            if (Valid)
                _assinaturas.Add(assinatura);
        }
        public override string ToString()
        {
            return $"{Nome.PrimeiroNome} {Nome.Sobrenome}";
        }
        #endregion
    }
}

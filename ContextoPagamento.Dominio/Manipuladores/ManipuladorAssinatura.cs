using ContextoPagamento.Compartilhado.Comandos;
using ContextoPagamento.Compartilhado.Manipuladores;
using ContextoPagamento.Dominio.Comandos;
using ContextoPagamento.Dominio.Entidades;
using ContextoPagamento.Dominio.Enums;
using ContextoPagamento.Dominio.Repositorio.Interfaces;
using ContextoPagamento.Dominio.Servicos;
using ContextoPagamento.Dominio.ValorObjetos;
using Flunt.Notifications;
using System;

namespace ContextoPagamento.Dominio.Manipuladores
{
    public class ManipuladorAssinatura : Notifiable,
        IManipulador<ComandoCriarAssinaturaBoleto>,
        IManipulador<ComandoCriarAssinaturaCartao>,
        IManipulador<ComandoCriarAssinaturaPayPal>
    {
        private readonly IAlunoRepositorio _alunoRepositorio;
        private readonly IEmailServico _emailServico;

        public ManipuladorAssinatura(
            IAlunoRepositorio alunoRepositorio,
            IEmailServico emailServico)
        {
            _alunoRepositorio = alunoRepositorio;
            _emailServico = emailServico;
        }

        public IComandoResultado Manipulador(ComandoCriarAssinaturaBoleto comando)
        {
            // Fail Fast Validate
            comando.Validar();
            if (comando.Invalid)
            {
                AddNotifications(comando);
                return new ComandoResultado(false, "Não foi possível realizar sua assinatura", comando);
            }

            // Verificar se Documento já esta Cadastrado
            if (_alunoRepositorio.DocumentoExiste(comando.Documento))
                AddNotification("Documento", "Este documento já está em uso!");

            // Verificar se Email ja esta cadastrado
            if (_alunoRepositorio.EmailExiste(comando.Documento))
                AddNotification("Email", "Este e-mail já está em uso!");

            //  Gerar os VOs (Valor Objeto)
            var documento = new Documento(comando.Documento, eTipoDocumento.CPF);
            var email = new Email(comando.Email);
            var nome = new Nome(comando.PrimeiroNome, comando.Sobrenome);
            var endereco = new Endereco(comando.Descricao, comando.Numero, comando.Bairro, comando.Cidade, comando.Estado, comando.Pais, comando.Cep);

            // Gerar Entidades
            var aluno = new Aluno(nome, documento, email);
            var assinatura = new Assinatura(DateTime.Now.AddMonths(1));
            var pagamento = new PagamentoBoleto(
                comando.CodigoBarras,
                comando.NossoNumero,
                comando.DataPagamento,
                comando.DataExpiracao,
                comando.ValorTotal,
                comando.ValorPago,
                comando.Pagador,
                new Documento(comando.PagadorDocumento, comando.PagadorTipoDocumento),
                endereco, // Endereco correto é do pagador (comando. Informacoes do pagador)
                email);

            // Relacionamentos
            assinatura.AdicionarPagamento(pagamento);
            aluno.AdicionarAssinatura(assinatura);

            // Aplicar Validações
            AddNotifications(nome, documento, email, endereco, aluno, assinatura, pagamento);

            // Checar Notificacoes
            if (Invalid)
                return new ComandoResultado(false, "Não foi possível realizar sua assinatura!", aluno);

            // Salvar Informacoes
            _alunoRepositorio.CriarAssinatura(aluno);

            // Enviar Email de boas-vindas
            _emailServico.Enviar(aluno.Nome.ToString(), aluno.Email.Endereco, "Bem Vindo!", "Sua assinatura foi criada!");

            // Retornar informacoes
            return new ComandoResultado(true, "Assinatura realizada com sucesso!", aluno);
        }
        public IComandoResultado Manipulador(ComandoCriarAssinaturaCartao comando)
        {
            // Segue a mesma idéia das outras formas de pagamento, mudando somente a regra para cada pagamento
            throw new NotImplementedException();
        }
        public IComandoResultado Manipulador(ComandoCriarAssinaturaPayPal comando)
        {
            // Segue a mesma idéia das outras formas de pagamento, mudando somente a regra para cada pagamento
            throw new NotImplementedException();
        }
    }
}

using ContextoPagamento.Compartilhado.Comandos;
using ContextoPagamento.Dominio.Enums;
using Flunt.Notifications;
using System;

namespace ContextoPagamento.Dominio.Comandos
{
    public class ComandoCriarAssinaturaPayPal : Notifiable, IComando
    {
        // Aluno
        public string PrimeiroNome { get; set; }
        public string Sobrenome { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }

        // PayPal
        public string CodigoTransacao { get; set; }

        // Pagamento
        public string PaymentNumber { get; set; }
        public DateTime DataPagamento { get; set; }
        public DateTime DataExpiracao { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorPago { get; set; }
        public string Pagador { get; set; }
        public string PagadorDocumento { get; set; }
        public eTipoDocumento PagadorTipoDocumento { get; set; }
        public string PagadorEmail { get; set; }

        // Endereco
        public string Descricao { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string Cep { get; set; }

        public void Validar()
        {
            // Aqui poderiamos colocar as Fail Fast Validation - Validação de Falha Rápida
            // poderia ser usado o AddNotifications do Flunt, etc
            // A ideia das FailsFast é recomendada para ganho de performance, e diminuição de requests no banco.            
            throw new NotImplementedException();
        }
    }
}

using ContextoPagamento.Dominio.Entidades;
using ContextoPagamento.Dominio.Repositorio.Interfaces;
using System;

namespace ContextoPagamento.Testes.Fakes
{
    public class FakeDescontoRepositorio : IDescontoRepositorio
    {
        public Desconto Obter(string codigo)
        {
            if (codigo == "12345678")
                return new Desconto(10, DateTime.Now.AddHours(2));

            if (codigo == "11111111")
                return new Desconto(10, DateTime.Now.AddHours(-2));

            return null;
        }
    }
}

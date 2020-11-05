using ContextoPagamento.Dominio.Entidades;
using System;
using System.Collections.Generic;

namespace ContextoPagamento.Dominio.Repositorio.Interfaces
{
    public interface IProdutoRepositorio
    {
        IEnumerable<Produto> Obter(IEnumerable<Guid> ids);
    }
}

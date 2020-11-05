using ContextoPagamento.Dominio.Comandos;
using System;
using System.Collections.Generic;

namespace ContextoPagamento.Dominio.Lib
{
    public static class ExtrairGuids
    {
        public static IEnumerable<Guid> Extrair(IList<ComandoCriarPedidoItem> itens)
        {
            var guids = new List<Guid>();
            foreach (var item in itens)
            {
                guids.Add(item.Produto);
            }

            return guids;
        }
    }
}

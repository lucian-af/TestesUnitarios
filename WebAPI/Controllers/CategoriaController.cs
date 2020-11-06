using ContextoPagamento.Dominio.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    // Endpoint == URL
    // http://localhost:5000
    // https://localhost:5001
    [Route("categorias")]
    public class CategoriaController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IList<Categoria>>> ObterCategorias()
        {
            return new List<Categoria>();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Categoria>> ObterCategoriasPorId(int id)
        {
            return new Categoria();
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Categoria>> IncluirCategoria([FromBody] Categoria categoria)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categoria);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Categoria>> AlterarCategoria(int id, [FromBody] Categoria categoria)
        {
            if (categoria.IdCategoria != id)
                return NotFound(new { message = "Categoria não encontrada" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categoria);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Categoria>> ExcluirCategoria(int id)
        {
            return Ok();
        }
    }
}

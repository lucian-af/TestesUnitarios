using ContextoPagamento.Dominio.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Dados;

namespace WebAPI.Controllers
{
    // Endpoint == URL
    // http://localhost:5000
    // https://localhost:5001
    [Route("v1/categorias")]
    public class CategoriaController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IList<Categoria>>> ObterCategorias([FromServices] DataContext context)
        {
            return await context.Categorias.AsNoTracking().ToListAsync();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Categoria>> ObterCategoriasPorId(int id, [FromServices] DataContext context)
        {
            return await context.Categorias.FindAsync(id);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Categoria>> IncluirCategoria(
            [FromBody] Categoria categoria,
            [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {

                context.Categorias.Add(categoria);
                await context.SaveChangesAsync();

                return Ok(categoria);
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível criar a categoria" });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Categoria>> AlterarCategoria(
            int id,
            [FromBody] Categoria categoria,
            [FromServices] DataContext context)
        {
            if (categoria.IdCategoria != id)
                return NotFound(new { message = "Categoria não encontrada" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Entry(categoria).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(categoria);
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível alterar a categoria" });
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Categoria>> ExcluirCategoria(
            int id,
            [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Categorias.Remove(await context.Categorias.FindAsync(id));
                await context.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível excluir a categoria" });
            }
        }
    }
}

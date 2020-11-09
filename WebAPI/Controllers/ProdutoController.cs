using ContextoPagamento.Dominio.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dados;

namespace WebAPI.Controllers
{
    [Route("v1/produtos")]
    public class ProdutoController : ControllerBase
    {
        private readonly DataContext _context;

        public ProdutoController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IList<Produto>>> ObterProdutos()
        {
            return await _context.Produtos
                .Include(pro => pro.Categoria)
                .AsNoTracking()
                .ToListAsync();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Produto>> ObterProdutosPorId(int id)
        {
            return await _context.Produtos
                .Include(pro => pro.Categoria)
                .FirstOrDefaultAsync(pro => pro.IdProduto == id);
        }

        [HttpGet]
        [Route("categoria/{id:int}")]
        public async Task<ActionResult<IList<Produto>>> ObterProdutosPorCategoria(int id)
        {
            return await _context.Produtos
                .Include(pro => pro.Categoria)
                .Where(pro => pro.CategoriaId == id)
                .AsNoTracking()
                .ToListAsync();
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Produto>> Inserir(
            [FromBody] Produto produto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _context.Produtos.Add(produto);
                await _context.SaveChangesAsync();
                return Ok(produto);
            }
            catch
            {
                return BadRequest("Não foi possível criar o produto");
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Produto>> Alterar(
            int id,
            [FromBody] Produto produto)
        {
            if (produto.IdProduto != id)
                return BadRequest("Produto não encontrado");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _context.Entry(produto).State = EntityState.Modified;                
                await _context.SaveChangesAsync();
                return Ok(produto);
            }
            catch
            {
                return BadRequest("Não foi possível alterar o produto");
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> Excluir(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _context.Produtos.Remove(await _context.Produtos.FindAsync(id));
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return BadRequest("Não foi possível excluir o produto");
            }
        }
    }
}

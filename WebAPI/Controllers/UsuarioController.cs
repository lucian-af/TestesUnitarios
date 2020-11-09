using AutoMapper;
using ContextoPagamento.Dominio.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dados;
using WebAPI.Models;
using WebAPI.Servico;

namespace WebAPI.Controllers
{
    [Route("v1/usuario")]
    public class UsuarioController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UsuarioController(
            DataContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        [Authorize(Roles = "adm")]
        // Essa anotação habilita o cache individual para cada situação, caso o ResponseCache esteja desabilitado no Startup
        [ResponseCache(VaryByHeader = "User-Agent", Location = ResponseCacheLocation.Any, Duration = 30)]
        // Essa anotação desabilita somente para este método, caso o ResponseCache esteja habilitado no Startup
        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<ActionResult<IList<UsuarioDto>>> ObterUsuarios()
        {
            return _mapper.Map<List<UsuarioDto>>(
                await _context.Usuarios
                    .AsNoTracking()
                    .ToListAsync());
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("")]
        public async Task<ActionResult<UsuarioDto>> Inserir([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                usuario.Perfil = "funcionario";
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                return Ok(_mapper.Map<UsuarioDto>(usuario));
            }
            catch
            {
                return BadRequest("Não foi possível criar o usuário");
            }
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<UsuarioDto>> Autenticar([FromBody] Usuario model)
        {
            var usuario = await _context.Usuarios
                .AsNoTracking()
                .Where(usr => usr.Nome == model.Nome && usr.Senha == model.Senha)
                .FirstOrDefaultAsync();

            if (usuario == null)
                return NotFound("Usuário inválido");

            var token = ServicoToken.GerarToken(usuario);

            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);
            usuarioDto.Token = token;

            return usuarioDto;
        }

        [HttpPut]
        [Authorize(Roles = "adm")]
        [Route("{id:int}")]
        public async Task<ActionResult<UsuarioDto>> Atualizar(
            int id,
            [FromBody] Usuario usuario)
        {
            if (usuario.IdUsuario != id)
                return BadRequest("Usuário não encontrado");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _context.Entry(usuario).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(_mapper.Map<UsuarioDto>(usuario));
            }
            catch
            {
                return BadRequest("Não foi possível alterar o usuário");
            }
        }

        [HttpDelete]
        [Authorize(Roles = "adm")]
        [Route("{id:int}")]
        public async Task<ActionResult> Excluir(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _context.Usuarios.Remove(await _context.Usuarios.FindAsync(id));
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return BadRequest("Não foi possível alterar o usuário");
            }
        }

        // Exempos de Autorizações com Roles
        //[HttpGet]
        //[Route("anonimo")]
        //[AllowAnonymous]
        //public string Anonimo() => "anonimo";

        //[HttpGet]
        //[Route("autenticado")]
        //[Authorize]
        //public string Autenticado() => "autenticado";

        //[HttpGet]
        //[Route("funcionario")]
        //[Authorize(Roles = "comum, adm")]
        //public string Funcionario() => "funcionario";

        //[HttpGet]
        //[Route("gerente")]
        //[Authorize(Roles = "adm")]
        //public string Gerente() => "gerente";
    }
}
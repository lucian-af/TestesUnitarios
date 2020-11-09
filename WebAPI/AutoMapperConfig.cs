using AutoMapper;
using ContextoPagamento.Dominio.Modelos;
using WebAPI.Models;
using WebAPI.Servico;

namespace WebAPI
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Usuario, UsuarioDto>()
                .ForMember(a => a.IdUsuario, b => b.MapFrom(c => c.IdUsuario))
                .ForMember(a => a.Nome, b => b.MapFrom(c => c.Nome))
                .ForMember(a => a.Perfil, b => b.MapFrom(c => c.Perfil))
                .ForMember(a => a.Token, b => b.Ignore());
        }
    }
}

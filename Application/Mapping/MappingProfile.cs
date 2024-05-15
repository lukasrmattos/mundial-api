using Application.DTO;
using AutoMapper;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(dest => dest.Perfil, opt => opt.MapFrom(src => src.Perfil));
            CreateMap<UsuarioDTO, Usuario>()
                .ForMember(dest => dest.Perfil, opt => opt.Ignore());

            CreateMap<Perfil, PerfilDTO>();
            CreateMap<PerfilDTO, Perfil>();
        }
    }
}

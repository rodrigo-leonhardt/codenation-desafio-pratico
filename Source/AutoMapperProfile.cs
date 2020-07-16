using AutoMapper;
using Source.DTOs;
using Source.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Source
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TipoLog, TipoLogDTO>().ReverseMap();
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
            CreateMap<Ambiente, AmbienteDTO>().ReverseMap();
            CreateMap<Log, LogInserirDTO>().ReverseMap();
        }
    }
}

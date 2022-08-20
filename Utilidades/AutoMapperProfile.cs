using AutoMapper;
using WebApiAutosCDK.DTOs;
using WebApiAutosCDK.Entidades;

namespace WebApiAutosCDK.Utilidades
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<MarcaCreacionDTOs, MarcaCDK>();
            CreateMap<ModeloCreacionDTOs, ModeloCDK>();
            CreateMap<MarcaCDK, MarcaDTOs>();
            CreateMap<ModeloCDK, ModeloDTOs>();
            CreateMap<ComentariosCreacionDTOs, Comentario>();
            CreateMap<Comentario, ComentarioDTOs>();
            CreateMap<VersionCDK, VersionDTOs>();
            CreateMap<VersionCreacionDTOs, VersionCDK>();
            CreateMap<ExtraCDK, ExtraDTOs>();
        }
    }
}

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
            CreateMap<MarcaEditarDTOs, MarcaCDK>();
            CreateMap<ModeloCDK, ModeloDTOs>();
            CreateMap<ModeloEditarDTOs, ModeloCDK>();
            CreateMap<ComentariosCreacionDTOs, Comentario>();
            CreateMap<Comentario, ComentarioDTOs>();
        }
    }
}

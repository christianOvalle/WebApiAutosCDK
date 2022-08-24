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
            CreateMap<VersionCreacionDTOs, VersionCDK>().ForMember(x => x.versionCDK_ExtraCDKs, opciones => opciones.MapFrom(MapVersionExtras));
            CreateMap<ExtraCDK, ExtraDTOs>();
            CreateMap<ExtraCreacionDTOs, ExtraCDK>();
        }

        private List<VersionCDK_ExtraCDK> MapVersionExtras(VersionCreacionDTOs versionCreacionDTOs, VersionCDK versionCDK )
        {
            var resultado = new List<VersionCDK_ExtraCDK>();

            if(versionCreacionDTOs.ExtrasIds == null) { return resultado; }

            foreach (var extraId in versionCreacionDTOs.ExtrasIds)
            {
                resultado.Add(new VersionCDK_ExtraCDK() { ExtrasCDKId = extraId });
            }

            return resultado;
        }
    }
}

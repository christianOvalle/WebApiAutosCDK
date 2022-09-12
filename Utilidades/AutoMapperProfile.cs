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
            CreateMap<MarcaCDK, MarcaDTOsConModelos>();
            CreateMap<ModeloCDK, ModeloDTOs>();
            CreateMap<ComentariosCreacionDTOs, Comentario>();
            CreateMap<Comentario, ComentarioDTOs>();
            CreateMap<VersionCDK, VersionDTOs>();
            CreateMap<VersionCDK, VersionDTOsConExtras>().ForMember(x => x.extraDTs, opciones => opciones.MapFrom(MapVersionDTOExtras));
            CreateMap<VersionCreacionDTOs, VersionCDK>().ForMember(x => x.versionCDK_ExtraCDKs, opciones => opciones.MapFrom(MapVersionExtras));
            CreateMap<ExtraCDK, ExtraDTOs>();
            CreateMap<ModeloPacthDTO, ModeloCDK>().ReverseMap();
            CreateMap<ExtraCDK, ExtraDTOsConVersiones>().ForMember(x => x.VersionDTOs, opciones => opciones.MapFrom(MapExtraDTOversion));
            CreateMap<ExtraCreacionDTOs, ExtraCDK>();
        }

        private List<VersionDTOs> MapExtraDTOversion(ExtraCDK extraCDK, ExtraDTOs extraDTOs )
        {
            var resultado = new List<VersionDTOs>();

            if(extraCDK.versionCDK_ExtraCDK == null) { return resultado; }

            foreach (var extraVersion in extraCDK.versionCDK_ExtraCDK)
            {
                resultado.Add(new VersionDTOs()
                {
                    Id = extraVersion.VersionCDKId,
                    versionNombre = extraVersion.version.versionNombre,
                    combustible = extraVersion.version.combustible,
                    potencia = extraVersion.version.potencia,
                    precioBase = extraVersion.version.precioBase
                });
            
            }
            return resultado;
        }


        private List<ExtraDTOs> MapVersionDTOExtras(VersionCDK versionCDK, VersionDTOs versionDTOs )
        {
            var resultado = new List<ExtraDTOs>();

            if (versionCDK.versionCDK_ExtraCDKs == null) { return resultado; }

            foreach (var extraVersion in versionCDK.versionCDK_ExtraCDKs)
            {
                resultado.Add(new ExtraDTOs()
                {
                    Id = extraVersion.ExtraCDKId,
                    nombre = extraVersion.Extra.nombre,
                    descripcion = extraVersion.Extra.descripcion
                });
            }

            return resultado;
        }

        private List<VersionCDK_ExtraCDK> MapVersionExtras(VersionCreacionDTOs versionCreacionDTOs, VersionCDK versionCDK )
        {
            var resultado = new List<VersionCDK_ExtraCDK>();

            if(versionCreacionDTOs.ExtrasIds == null) { return resultado; }

            foreach (var extraId in versionCreacionDTOs.ExtrasIds)
            {
                resultado.Add(new VersionCDK_ExtraCDK() { ExtraCDKId = extraId });
            }

            return resultado;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using WebApiAutosCDK.DTOs;
using WebApiAutosCDK.Servicios;

namespace WebApiAutosCDK.Utilidades
{
    public class HATEOASMarcaFilterAttribute : HATEOASFiltroAttribute
    {
        private readonly GeneradorEnlaces generadorEnlaces;

        public HATEOASMarcaFilterAttribute(GeneradorEnlaces generadorEnlaces)
        {
            this.generadorEnlaces = generadorEnlaces;
        }

        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var debeIncluir = DebeIncluirHATEOAS(context);

            if (!debeIncluir)
            {
                await next();
                return;
            }

            var resultado = context.Result as ObjectResult;
            var maraDTOs = resultado.Value as MarcaDTOs;
            if (maraDTOs == null)
            {

                var marcaDTO = resultado.Value as List<MarcaDTOs> ?? throw new ArgumentException("Se esperaba una instancia de marcaDTOs o List<marcaDTOs>");

                marcaDTO.ForEach(async x => await generadorEnlaces.GenerarEnlaces(x));
                resultado.Value = marcaDTO;

            }
            else
            {
                await generadorEnlaces.GenerarEnlaces(maraDTOs);
            }
            
            await next();
        }

    }
}

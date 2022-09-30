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
            var modelo = resultado.Value as MarcaDTOs ?? throw new ArgumentNullException("Se  esperaba una instancia de MarcaDTOs");
            await generadorEnlaces.GenerarEnlaces(modelo);s
            await next();
        }

    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using WebApiAutosCDK.DTOs;

namespace WebApiAutosCDK.Servicios
{
    public class GeneradorEnlaces
    {
        private readonly IAuthorizationService authorizationService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IActionContextAccessor actionContextAccessor;

        public GeneradorEnlaces(IAuthorizationService authorizationService, IHttpContextAccessor httpContextAccessor, IActionContextAccessor actionContextAccessor)
        {
            this.authorizationService = authorizationService;
            this.httpContextAccessor = httpContextAccessor;
            this.actionContextAccessor = actionContextAccessor;
        }

        private async Task<bool> EsAdmin()
        {
            var httpContext = httpContextAccessor.HttpContext;
            var resultado = await authorizationService.AuthorizeAsync(httpContext.User, "EsAdmin");
            return resultado.Succeeded;
        }

        private IUrlHelper ContruirUrlHelper()
        {
            var factoria = httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IUrlHelperFactory>();
            return factoria.GetUrlHelper(actionContextAccessor.ActionContext);
        }

        public async Task GenerarEnlaces(MarcaDTOs marcaDTOs)
        {
            var esAdmin = await EsAdmin();
            var url = ContruirUrlHelper();

            marcaDTOs.Enlaces.Add(new DatoHATEOAS(enlace: Url.Link("obtenerMarca", new { id = marcaDTOs.Id }), descripcion: "self", metodo: "GET"));

            if (esAdmin)
            {
                marcaDTOs.Enlaces.Add(new DatoHATEOAS(enlace: Url.Link("actualizarMarca", new { id = marcaDTOs.Id }), descripcion: "marca-actualizar", metodo: "PUT"));
                marcaDTOs.Enlaces.Add(new DatoHATEOAS(enlace: Url.Link("borrarMarca", new { id = marcaDTOs.Id }), descripcion: "marca-borrar", metodo: "DELETE"));
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using WebApiAutosCDK.DTOs;

namespace WebApiAutosCDK.Controllers
{
    [ApiController]
    [Route("api")]
    public class RootController : ControllerBase
    {
        [HttpGet(Name ="ObtenerRoot")]
        public ActionResult<IEnumerable<DatoHATEOAS>> Get()
        {
            var datosHateoas = new List<DatoHATEOAS>();

            datosHateoas.Add(new DatoHATEOAS(enlace: Url.Link("ObtenerRoot", new { }), descripcion: "self", metodo: "GET"));

            return datosHateoas;
        }
    }
}

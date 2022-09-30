﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiAutosCDK.DTOs;

namespace WebApiAutosCDK.Controllers
{
    [ApiController]
    [Route("api")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RootController : ControllerBase
    {
        private readonly IAuthorizationService authorizationService;

        public RootController(IAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
        }

        [HttpGet(Name ="ObtenerRoot")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<DatoHATEOAS>>> Get()
        {
            var esAdmin = await authorizationService.AuthorizeAsync(User, "esAdmin");

            var datosHateoas = new List<DatoHATEOAS>();

            datosHateoas.Add(new DatoHATEOAS(enlace: Url.Link("ObtenerRoot", new { }), descripcion: "self", metodo: "GET"));

            datosHateoas.Add(new DatoHATEOAS(enlace: Url.Link("obtenerMarcas", new { }), descripcion: "marcas", metodo: "GET"));

            if (esAdmin.Succeeded)
            {
                datosHateoas.Add(new DatoHATEOAS(enlace: Url.Link("crearMarca", new { }), descripcion: "marcas-crear", metodo: "POST"));

                datosHateoas.Add(new DatoHATEOAS(enlace: Url.Link("crearModelo", new { }), descripcion: "modelo-crear", metodo: "POST"));
            }
            return datosHateoas;
        }
    }
}

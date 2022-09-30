using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutosCDK.DTOs;
using WebApiAutosCDK.Entidades;
using WebApiAutosCDK.Utilidades;

namespace WebApiAutosCDK.Controllers
{
    [ApiController]
    [Route("api/marca")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy ="EsAdmin")]
    public class MarcaCDKController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAuthorizationService authorizationService;

        public MarcaCDKController(ApplicationDbContext context, IMapper mapper, IAuthorizationService authorizationService)
        {
            this.context = context;
            this.mapper = mapper;
            this.authorizationService = authorizationService;
        }

        [HttpGet(Name ="obtenerMarcas")]
        [AllowAnonymous]
        public async Task<ActionResult<ColeccionDeRecursos<MarcaDTOs>>> Get([FromQuery] bool incluirHATEOAS)
        {
            var marcasLista = await context.MarcasCDK.Include(x=>x.comentarios).Include(x=>x.Modelos).ToListAsync();
            var dtos = mapper.Map<List<MarcaDTOs>>(marcasLista);
            var esAdmin = await authorizationService.AuthorizeAsync(User, "esAdmin");
            if (incluirHATEOAS) {
              
                    //dtos.ForEach(dto => GenerarEnlaces(dto, esAdmin.Succeeded));

                var resultado = new ColeccionDeRecursos<MarcaDTOs> { valores = dtos };
                resultado.Enlaces.Add(new DatoHATEOAS(enlace: Url.Link("obtenerMarcas", new { }), descripcion: "self", metodo: "GET"));
                if (esAdmin.Succeeded)
                {
                    resultado.Enlaces.Add(new DatoHATEOAS(enlace: Url.Link("crearMarcas", new { }), descripcion: "crear-marca", metodo: "POST"));
                }
                return Ok(resultado);
            }
            return Ok(dtos);
        }

        [HttpGet("{id:int}", Name ="obtenerMarca")]
        [AllowAnonymous]
        [ServiceFilter(typeof(HATEOASMarcaFilterAttribute))]
        public async Task<ActionResult<MarcaDTOsConModelos>> Get(int id, [FromHeader] string incluirHATEOAS)
        {
            var marca = await context.MarcasCDK.Include(x=>x.comentarios).Include(x=>x.Modelos).FirstOrDefaultAsync(x => x.Id == id);

            if (marca == null)
            {
                return NotFound();
            }

            var dto = mapper.Map<MarcaDTOs>(marca);
            return Ok(dto);
        }

        

        [HttpGet("{nombre}", Name ="obtenerMarcaPorNombre")]
        public async Task<ActionResult<List<MarcaDTOs>>> Get(string nombre)
        {
            var existe = await context.MarcasCDK.Where(x => x.marca.Contains(nombre)).ToListAsync();

            if (existe.Count == 0)
            {
                return BadRequest($"No se a encontrado registros que contengan ({nombre})");
            }

            return mapper.Map<List<MarcaDTOs>>(existe);

        }

        [HttpPost(Name ="crearMarca")]
        public async Task<ActionResult> Post(MarcaCreacionDTOs marcaCreacionDTOs)
        {
            var existe = await context.MarcasCDK.AnyAsync(x => x.marca == marcaCreacionDTOs.marca);

            if (existe)
            {
                return BadRequest($"La marca {marcaCreacionDTOs.marca} ya existe en el registro");
            }

            var marcaIngresar = mapper.Map<MarcaCDK>(marcaCreacionDTOs);

            context.Add(marcaIngresar);
            await context.SaveChangesAsync();

            var marca = mapper.Map<MarcaDTOs>(marcaIngresar);


            return CreatedAtRoute("obtenerMarca", new { id = marca.Id }, marca);
        }

        [HttpPut("{id:int}", Name ="actualizarMarca")]
        public async Task<ActionResult> Put(MarcaCreacionDTOs marcaCreacionDTOs, int id)
        {
           
            var existe = await context.MarcasCDK.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            var marcaCDK = mapper.Map<MarcaCDK>(marcaCreacionDTOs);
            marcaCDK.Id = id;

            context.Update(marcaCDK);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}", Name ="borrarMarca")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.MarcasCDK.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new MarcaCDK { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}

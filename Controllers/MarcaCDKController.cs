using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutosCDK.DTOs;
using WebApiAutosCDK.Entidades;

namespace WebApiAutosCDK.Controllers
{
    [ApiController]
    [Route("api/marca")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy ="EsAdmin")]
    public class MarcaCDKController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public MarcaCDKController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<MarcaDTOsConModelos>>> Get()
        {
            var marcasLista = await context.MarcasCDK.Include(x => x.Modelos).ToListAsync();
            return mapper.Map<List<MarcaDTOsConModelos>>(marcasLista);

        }

        [HttpGet("{id:int}", Name ="Obtener marca")]
        public async Task<ActionResult<MarcaDTOsConModelos>> Get(int id)
        {
            var marca = await context.MarcasCDK.Include(x=>x.comentarios).Include(x=>x.Modelos).FirstOrDefaultAsync(x => x.Id == id);

            if (marca == null)
            {
                return NotFound();
            }

            return mapper.Map<MarcaDTOsConModelos>(marca);

        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<List<MarcaDTOs>>> Get(string nombre)
        {
            var existe = await context.MarcasCDK.Where(x => x.marca.Contains(nombre)).ToListAsync();

            if (existe.Count == 0)
            {
                return BadRequest($"No se a encontrado registros que contengan ({nombre})");
            }

            return mapper.Map<List<MarcaDTOs>>(existe);

        }

        [HttpPost]
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


            return CreatedAtRoute("Obtener marca", new { id = marca.Id }, marca);
        }

        [HttpPut("{id:int}")]
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

        [HttpDelete("{id:int}")]
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

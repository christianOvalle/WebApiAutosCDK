using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutosCDK.DTOs;
using WebApiAutosCDK.Entidades;

namespace WebApiAutosCDK.Controllers
{
    [ApiController]
    [Route("api/marca")]
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
        public async Task<ActionResult<List<MarcaDTOs>>> Get()
        {
            var marcasLista = await context.MarcasCDK.Include(x => x.ModelosCDK).ToListAsync();
            return mapper.Map<List<MarcaDTOs>>(marcasLista);

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MarcaDTOs>> Get(int id)
        {
            var marca = await context.MarcasCDK.FirstOrDefaultAsync(x => x.Id == id);

            if (marca == null)
            {
                return NotFound();
            }

            return mapper.Map<MarcaDTOs>(marca);

        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<List<MarcaDTOs>>> Get(string nombre)
        {
            var existe = await context.MarcasCDK.Where(x => x.marca.Contains(nombre)).ToListAsync();

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

            var autor = mapper.Map<MarcaCDK>(marcaCreacionDTOs);

            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(MarcaEditarDTOs marcaEditarDTOs, int id)
        {
            if (marcaEditarDTOs.Id != id)
            {
                return BadRequest("El id de la marca NO coincide con el de la URL");
            }

            var existe = await context.MarcasCDK.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            var marca = mapper.Map<MarcaCDK>(marcaEditarDTOs);

            context.Update(marca);
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

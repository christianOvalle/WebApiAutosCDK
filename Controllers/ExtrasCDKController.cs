using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutosCDK.DTOs;
using WebApiAutosCDK.Entidades;

namespace WebApiAutosCDK.Controllers
{
    [ApiController]
    [Route("api/extra")]
    public class ExtrasCDKController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ExtrasCDKController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet(Name ="obtenerExtras")]
        public async Task<ActionResult<List<ExtraDTOs>>> Get()
        {
            var extras = await context.ExtraCDK.ToListAsync();
            return mapper.Map<List<ExtraDTOs>>(extras);
        }

        [HttpGet("{id:int}", Name ="obtenerExtra")]
        public async Task<ActionResult<ExtraDTOsConVersiones>> Get(int id)
        {
            var existe = await context.ExtraCDK.Include(x=>x.versionCDK_ExtraCDK).ThenInclude(x=>x.version).FirstOrDefaultAsync(x => x.Id == id);

            if (existe == null)
            {
                return NotFound();
            }

            return mapper.Map<ExtraDTOsConVersiones>(existe);

           
        }

        [HttpGet("{nombre}", Name ="obtenerExtraPorNombre")]
        public async Task<ActionResult<List<ExtraDTOs>>> Get(string nombre)
        {
            var existe = await context.ExtraCDK.Where(x=>x.nombre.Contains(nombre)).ToListAsync();

            if(existe.Count == 0)
            {
                return BadRequest($"No se a encontrado registros que contengan ({nombre})");
            }

            var extra = mapper.Map<List<ExtraDTOs>>(existe);

            return extra;
        }

        [HttpPost(Name ="crearExtra")]
        public async Task<ActionResult> Post(ExtraCreacionDTOs extraCDK)
        {
            var existe = await context.ExtraCDK.AnyAsync(x => x.nombre == extraCDK.nombre);

            if (existe)
            {
                return BadRequest($"El extra {extraCDK.nombre} ya existe");
            }

            var extra = mapper.Map<ExtraCDK>(extraCDK);

            context.Add(extra);
            await context.SaveChangesAsync();

            var extradto = mapper.Map<ExtraDTOs>(extra);

            return CreatedAtRoute("obtenerExtra", new { id = extradto.Id }, extradto);
        }
        
        [HttpPut("{id:int}", Name ="actualizarExtra")]
        public async Task<ActionResult> Put(ExtraCreacionDTOs extraCreacionDTO, int id)
        {
         
            var existe = await context.ExtraCDK.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            var extraEditado = mapper.Map<ExtraCDK>(extraCreacionDTO);
            extraEditado.Id = id;

            context.Update(extraEditado);
            await context.SaveChangesAsync();
            return NoContent();
        }
        
        [HttpDelete("{id:int}", Name ="borrarExtra")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.ExtraCDK.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new ExtraCDK { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }

    }
}

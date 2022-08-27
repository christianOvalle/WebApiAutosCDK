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

        [HttpGet]
        public async Task<ActionResult<List<ExtraDTOs>>> Get()
        {
            var extras = await context.ExtraCDK.ToListAsync();
            return mapper.Map<List<ExtraDTOs>>(extras);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ExtraDTOs>> Get(int id)
        {
            var existe = await context.ExtraCDK.Include(x=>x.versionCDK_ExtraCDK).ThenInclude(x=>x.version).FirstOrDefaultAsync(x => x.Id == id);

            if (existe == null)
            {
                return NotFound();
            }

            var extra = mapper.Map<ExtraDTOs>(existe);

            return extra;
        }

        [HttpGet("{nombre}")]
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

        [HttpPost]
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
            return Ok();
        }
        
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(ExtraCDK extraCDK, int id)
        {
            if(extraCDK.Id != id)
            {
                return BadRequest("El id del extra no coincide con el de la Url");
            }

            var existe = await context.ExtraCDK.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            context.Update(extraCDK);
            await context.SaveChangesAsync();
            return Ok();
        }
        
        [HttpDelete("{id:int}")]
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

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutosCDK.DTOs;
using WebApiAutosCDK.Entidades;

namespace WebApiAutosCDK.Controllers
{
    [ApiController]
    [Route("api/version")]
    public class VersionCDKController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public VersionCDKController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<VersionDTOs>>> Get()
        {
            var versiones=  await context.VersionCDK.ToListAsync();
            return mapper.Map <List<VersionDTOs>>(versiones);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<VersionDTOs>> Get(int id)
        {
            var existe = await context.VersionCDK.Include(x=>x.versionCDK_ExtraCDKs).ThenInclude(x=>x.Extra).FirstOrDefaultAsync(x => x.Id == id);

            if (existe == null)
            {
                return NotFound();
            }

            return mapper.Map<VersionDTOs>(existe);
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<List<VersionDTOs>>> Get(string nombre)
        {
            var existe = await context.VersionCDK.Where(x => x.versionNombre.Contains(nombre)).ToListAsync();

            if (existe.Count == 0)
            {
                return BadRequest($"No existe un registro que contenga ({nombre})");
            }

            return mapper.Map<List<VersionDTOs>>(existe);
        }

        [HttpPost]
        public async Task<ActionResult> Post(VersionCreacionDTOs versionCDK)
        {
            var existeModelo = await context.ModelosCDK.AnyAsync(x => x.Id == versionCDK.ModeloCDKId);

            if (!existeModelo)
            {
                return BadRequest($"No existe el modelo de id {versionCDK.ModeloCDKId}");
            }

             
            var extrasIds = await context.ExtraCDK.Where(x => versionCDK.ExtrasIds.Contains(x.Id)).Select(x => x.Id).ToListAsync();

            if(versionCDK.ExtrasIds.Count != extrasIds.Count)
            {
                return BadRequest("No existe uno de los Extras enviados");
            }


            var version = mapper.Map<VersionCDK>(versionCDK);

            context.Add(version);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(VersionCDK versionCDK, int id)
        {
            if(versionCDK.Id != id)
            {
                return BadRequest("El id de la version a editar no coincide con el enviado en la url");
            }

            var existeVersion = await context.VersionCDK.AnyAsync(x => x.Id == id);

            if (!existeVersion)
            {
                return BadRequest($"La version de id {id} no existes");
            }

            context.Update(versionCDK);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.VersionCDK.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return BadRequest("El registro que intenta eliminar no existe");
            }

            context.Remove(new VersionCDK { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}

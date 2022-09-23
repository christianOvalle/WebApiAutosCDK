using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutosCDK.DTOs;
using WebApiAutosCDK.Entidades;

namespace WebApiAutosCDK.Controllers
{
    [ApiController]
    [Route("api/ubicacion")]
    public class UbicacionDireccionCDKController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public UbicacionDireccionCDKController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UbicacionDTOs>>Get(int id)
        {
            var ubicacion = await context.UbicacionesDireccionCDK.FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<UbicacionDTOs>(ubicacion);
        }

        [HttpPost]
        public async Task<ActionResult> Post(UbicacionCreacionDTOs ubicacionCreacionDTOs)
        {
            var ubicacion = await context.ClientesCDK.AnyAsync(x=>x.Id == ubicacionCreacionDTOs.ClienteCDKId);

            if (!ubicacion)
            {
                return BadRequest($"No existe un cliente de id {ubicacionCreacionDTOs.ClienteCDKId}");
            }

            var ubiMapeada = mapper.Map<UbicacionDireccionCDK>(ubicacionCreacionDTOs);

            context.Add(ubiMapeada);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, UbicacionCreacionDTOs ubicacionCreacionDTOs)
        {
            var ubicacion = await context.UbicacionesDireccionCDK.AnyAsync(x => x.Id == id);

            if (!ubicacion)
            {
                return BadRequest("No existe la ubicacion que intenta editar");
            }

            var ubicacionCliente = await context.ClientesCDK.AnyAsync(x => x.Id == ubicacionCreacionDTOs.ClienteCDKId);

            if (!ubicacionCliente)
            {
                return BadRequest($"No existe un cliente de id {ubicacionCreacionDTOs.ClienteCDKId}");
            }

            var ubiMapeada = mapper.Map<UbicacionDireccionCDK>(ubicacionCreacionDTOs);
            ubiMapeada.Id = id;

            context.Update(ubiMapeada);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("id:int")]
        public async Task<ActionResult> Delete(int id)
        {

            var ubicacion = await context.UbicacionesDireccionCDK.AnyAsync(x => x.Id == id);

            if (!ubicacion)
            {
                return BadRequest("No existe la ubicacion que intenta editar");
            }

            context.Remove(new UbicacionDireccionCDK { Id = id });
            await context.SaveChangesAsync();
            return NoContent();

        }
        
    }
}

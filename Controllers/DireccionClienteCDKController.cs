using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutosCDK.DTOs;
using WebApiAutosCDK.Entidades;

namespace WebApiAutosCDK.Controllers
{
    [ApiController]
    [Route("api/direccion")]
    public class DireccionClienteCDKController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public DireccionClienteCDKController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<DireccionDTOs>>> Get()
        {
            var direccionLista = await context.DireccionClientesCDK.ToListAsync();
            return mapper.Map<List<DireccionDTOs>>(direccionLista);
        }

        [HttpGet("{id:int}", Name = "Obtener Direccion Editada")]
        public async Task<ActionResult<DireccionDTOs>>Get(int id)
        {
            var direccionPorId = await context.DireccionClientesCDK.FirstOrDefaultAsync(x => x.Id == id);

            if(direccionPorId == null)
            {
                return BadRequest($"No existe una direccion de id {id}");
            }

            return mapper.Map<DireccionDTOs>(direccionPorId);
        }

        [HttpPost]
        public async Task<ActionResult> Post(DireccionCreacionDTOs direccionCreacionDTOs)
        {
            var existeUbicacion = await context.UbicacionesDireccionCDK.AnyAsync(x => x.Id == direccionCreacionDTOs.UbicacionDireccionCDKId);

            if (!existeUbicacion)
            {
                return BadRequest($"No existe una ubicacion de direccion de id {direccionCreacionDTOs.UbicacionDireccionCDKId}");
            }

            var existeCliente = await context.ClientesCDK.AnyAsync(x => x.Id == direccionCreacionDTOs.ClienteCDKId);

            if (!existeCliente)
            {
                return BadRequest($"No existe un cliente de id {direccionCreacionDTOs.ClienteCDKId}");
            }

            var direccionMapeada = mapper.Map<DireccionClienteCDK>(direccionCreacionDTOs);
            context.Add(direccionMapeada);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, DireccionCreacionDTOs direccionCreacionDTOs)
        {
            var existeDireccion = await context.DireccionClientesCDK.AnyAsync(x => x.Id == id);

            if (!existeDireccion)
            {
                return BadRequest("No existe la direccion que intenta editar");
            }

            var existeUbicacion = await context.UbicacionesDireccionCDK.AnyAsync(x => x.Id == direccionCreacionDTOs.UbicacionDireccionCDKId);

            if (!existeUbicacion)
            {
                return BadRequest($"No existe una ubicacion de direccion de id {direccionCreacionDTOs.UbicacionDireccionCDKId}");
            }

            var existeCliente = await context.ClientesCDK.AnyAsync(x => x.Id == direccionCreacionDTOs.ClienteCDKId);

            if (!existeCliente)
            {
                return BadRequest($"No existe un cliente de id {direccionCreacionDTOs.ClienteCDKId}");
            }

            var direccionMapeada = mapper.Map<DireccionClienteCDK>(direccionCreacionDTOs);
            direccionMapeada.Id = id;

            context.Update(direccionMapeada);
            await context.SaveChangesAsync();

            var direccion = mapper.Map<DireccionDTOs>(direccionMapeada);

            return CreatedAtAction("Obtener Direccion Editada", new { id = direccion.Id }, direccion);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existeDireccion = await context.DireccionClientesCDK.AnyAsync(x => x.Id == id);

            if (!existeDireccion)
            {
                return BadRequest("No existe la direccion que intenta editar");
            }

            context.Remove(new DireccionClienteCDK { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutosCDK.DTOs;
using WebApiAutosCDK.Entidades;

namespace WebApiAutosCDK.Controllers
{
    [ApiController]
    [Route("api/modelo")]
    public class ModeloCDKController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ModeloCDKController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ModeloDTOs>>> Get()
        {
            var modelos = await context.ModelosCDK.ToListAsync();
            return mapper.Map<List<ModeloDTOs>>(modelos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ModeloDTOs>> Get(int id)
        {
           var existe = await  context.ModelosCDK.FirstOrDefaultAsync(x => x.Id == id);

            if(existe == null)
            {
                return NotFound();
            }

            return mapper.Map<ModeloDTOs>(existe);
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<List<ModeloDTOs>>> Get(string nombre)
        {
            var existe = await context.ModelosCDK.Where(x => x.modelo.Contains(nombre)).ToListAsync();      
            return mapper.Map<List<ModeloDTOs>>(existe);
        }


        [HttpPost]
        public async Task<ActionResult> Post(ModeloCreacionDTOs modeloCreacionDTOs)
        {
            var validacion = await context.MarcasCDK.AnyAsync(x => x.Id == modeloCreacionDTOs.MarcaCDKId);

            if (!validacion)
            {
                return BadRequest($"No existe la marca de Id: {modeloCreacionDTOs.MarcaCDKId}");
            }

            var existe = await context.ModelosCDK.AnyAsync(x => x.modelo == modeloCreacionDTOs.modelo);

            if (existe)
            {
                return BadRequest($"Ya existe un modelo conel nombre {modeloCreacionDTOs.modelo}");
            }

            var modelo = mapper.Map<ModeloCDK>(modeloCreacionDTOs);

            context.Add(modelo);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(ModeloEditarDTOs modeloEditarDTOs, int id)
        {
            if(modeloEditarDTOs.Id != id)
            {
                return BadRequest("El id del modelo no coincide con el de la URL");
            }

            var valido = await context.ModelosCDK.AnyAsync(x=>x.Id == modeloEditarDTOs.Id);

            if (!valido)
            {
                return BadRequest("El modelo que desea editar no existe");
            }

            var existeMarca = await context.MarcasCDK.AnyAsync(x => x.Id == modeloEditarDTOs.MarcaCDKId);

            if (!existeMarca)
            {
                return BadRequest($"No existe una marca con id {modeloEditarDTOs.MarcaCDKId}");
            }

            var modelo = mapper.Map<ModeloCDK>(modeloEditarDTOs);

            context.Update(modelo);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var valido = await context.ModelosCDK.AnyAsync(x => x.Id == id);

            if (!valido)
            {
                return BadRequest("El modelo que desea borrar no existe");
            }

            context.Remove(new ModeloCDK() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{modelo}")]
        public async Task<ActionResult> Delete(string modelo)
        {
            var valido = await context.ModelosCDK.AnyAsync(x => x.modelo == modelo);

            if (!valido)
            {
                return BadRequest("El modelo que desea borrar no existe");
            }

            context.Remove(new ModeloCDK() { modelo = modelo });
            await context.SaveChangesAsync();
            return Ok();
        }

    }
}

using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpGet(Name ="obtenerModelos")]
        public async Task<ActionResult<List<ModeloDTOs>>> Get()
        {
            var modelos = await context.ModelosCDK.Include(x=>x.MarcaCDK).ToListAsync();
            return mapper.Map<List<ModeloDTOs>>(modelos);
        }

        [HttpGet("{id:int}", Name = "obtenerModelo")]
        public async Task<ActionResult<ModeloDTOs>> Get(int id)
        {
           var existe = await  context.ModelosCDK.Include(x=>x.MarcaCDK).FirstOrDefaultAsync(x => x.Id == id);

            if(existe == null)
            {
                return NotFound();
            }

            return mapper.Map<ModeloDTOs>(existe);
        }

        [HttpGet("{nombre}",Name = "obtenerModelosPorNombre")]
        public async Task<ActionResult<List<ModeloDTOs>>> Get(string nombre)
        {
            var existe = await context.ModelosCDK.Where(x => x.modelo.Contains(nombre)).ToListAsync();

            if (existe.Count == 0)
            {
                return BadRequest($"No se a encontrado registros que contengan ({nombre})");
            }

            return mapper.Map<List<ModeloDTOs>>(existe);
        }


        [HttpPost(Name ="crearModelo")]
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

            var modeloDTO = mapper.Map<ModeloDTOs>(modelo);

            return CreatedAtRoute("obtenerModelo", new { id = modeloDTO.Id }, modeloDTO);
        }

        [HttpPut("{id:int}", Name ="actualizarModelo")]
        public async Task<ActionResult> Put(ModeloCreacionDTOs modeloCreacionDTOs, int id)
        {

            var valido = await context.ModelosCDK.AnyAsync(x=>x.Id == id);

            if (!valido)
            {
                return BadRequest("El modelo que desea editar no existe");
            }

            var existeMarca = await context.MarcasCDK.AnyAsync(x => x.Id == modeloCreacionDTOs.MarcaCDKId);

            if (!existeMarca)
            {
                return BadRequest($"No existe una marca con id {modeloCreacionDTOs.MarcaCDKId}");
            }

            var modeloCDK = mapper.Map<ModeloCDK>(modeloCreacionDTOs);
            modeloCDK.Id = id;
 
            context.Update(modeloCDK);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}", Name ="borrarModelo")]
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

        [HttpDelete("{modelo}", Name ="borrarModeloPorNombre")]
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

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<ModeloPacthDTO> document)
        {
            if(document == null)
            {
                return BadRequest();
            }

            var modeloDB = await context.ModelosCDK.FirstOrDefaultAsync(x => x.Id == id);

            if (modeloDB == null)
            {
                return NotFound();
            }

            var modeloDTO = mapper.Map<ModeloPacthDTO>(modeloDB);

            document.ApplyTo(modeloDTO, ModelState);

            var valido = TryValidateModel(modeloDB);

            if (!valido)
            {
                return BadRequest();
            }

            mapper.Map(modeloDTO, modeloDB);

            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutosCDK.DTOs;
using WebApiAutosCDK.Entidades;

namespace WebApiAutosCDK.Controllers
{
    [ApiController]
    [Route("api/marca/{MarcaId:int}/comentarios")]
    public class ComentariosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ComentariosController(ApplicationDbContext context, IMapper mapper) 
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ComentarioDTOs>>> Get(int MarcaId)
        {
            var existe = await context.MarcasCDK.AnyAsync(x => x.Id == MarcaId);

            if (!existe)
            {
                return NotFound();
            }

            var comentarios = await context.Comentarios.Where(x=>x.MarcaCDKId == MarcaId).ToListAsync();

            return mapper.Map<List<ComentarioDTOs>>(comentarios);
        }

        [HttpPost]
        public async Task<ActionResult> Post(int marcaId, ComentariosCreacionDTOs comentariosCreacionDTOs)
        {
           var existe = await context.MarcasCDK.AnyAsync(x => x.Id == marcaId);

            if(!existe)
            {
                return NotFound();
            }

            var comentario = mapper.Map<Comentario>(comentariosCreacionDTOs);

            comentario.MarcaCDKId = marcaId;
            context.Add(comentario);
            await context.SaveChangesAsync();
            return Ok();

        }
    }
}

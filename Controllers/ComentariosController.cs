using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> userManager;

        public ComentariosController(ApplicationDbContext context, IMapper mapper, UserManager<IdentityUser> userManager) 
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet(Name ="obtenerComentarios")]
        public async Task<ActionResult<List<ComentarioDTOs>>> Get(int MarcaId)
        {
            var existeMarca = await context.MarcasCDK.AnyAsync(x => x.Id == MarcaId);

            if (!existeMarca)
            {
                return NotFound();
            }

            var comentarios = await context.Comentarios.Where(x=>x.MarcaCDKId == MarcaId).ToListAsync();

            return mapper.Map<List<ComentarioDTOs>>(comentarios);
        }

        [HttpGet("{id:int}", Name ="obtenerComentario")]
        public async Task<ActionResult<ComentarioDTOs>> GetPorId(int id)
        {
            var existeComentario = await context.Comentarios.FirstOrDefaultAsync(x => x.Id == id);

            if (existeComentario == null)
            {
                return NotFound();
            }

            return mapper.Map<ComentarioDTOs>(existeComentario);
        }

        [HttpPost(Name ="crearComentario")]
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post(int MarcaId, ComentariosCreacionDTOs comentariosCreacionDTOs)
        {
            var emailClaim = HttpContext.User.Claims.Where(x => x.Type == "email").FirstOrDefault();
            var email = emailClaim.Value;
            var usuario = await userManager.FindByEmailAsync(email);
            var usuarioId = usuario.Id;
            var existeMarca = await context.MarcasCDK.AnyAsync(x => x.Id == MarcaId);

            if(!existeMarca)
            {
                return NotFound();
            }

            var comentario = mapper.Map<Comentario>(comentariosCreacionDTOs);

            comentario.MarcaCDKId = MarcaId;
            comentario.UsuarioId = usuarioId;
            context.Add(comentario);
            await context.SaveChangesAsync();

            var ComentarioDTO = mapper.Map<ComentarioDTOs>(comentario);

            return CreatedAtRoute("obtenerComentario", new { id = comentario.Id, marcaId = MarcaId }, ComentarioDTO);

        }

        [HttpPut("{id:int}", Name ="actualizarComentario")]
        public async Task<ActionResult> Put(int MarcaId, int id, ComentariosCreacionDTOs comentarioDTOs)
        {
            var existeMarca = await context.MarcasCDK.AnyAsync(x => x.Id == MarcaId);

            if (!existeMarca)
            {
                return NotFound();
            }

            var comentarioExiste = await context.Comentarios.AnyAsync(x=>x.Id == id);

            if (!comentarioExiste)
            {
                return NotFound();
            }

            var mapeo = mapper.Map<Comentario>(comentarioDTOs);
            mapeo.Id = id;
            mapeo.MarcaCDKId = MarcaId;
            
            context.Update(mapeo);
            await context.SaveChangesAsync();
            return NoContent();

        }
        

    }
}

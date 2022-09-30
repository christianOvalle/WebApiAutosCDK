using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutosCDK.DTOs;
using WebApiAutosCDK.Entidades;

namespace WebApiAutosCDK.Controllers
{
    [ApiController]
    [Route("api/cliente")]
    public class ClienteCDKController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ClienteCDKController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet(Name ="obtenerClientes")]
        public async Task<ActionResult<List<ClienteDTOs>>> Get()
        {
            var clientes = await context.ClientesCDK.Include(x => x.direccionClienteCDKs).Include(x => x.ubicacionDireccionCDKs).ToListAsync();
            return mapper.Map<List<ClienteDTOs>>(clientes);
        }

        [HttpGet("{id:int}", Name ="obtenerCliente")]
        public async Task<ActionResult<ClienteDTOs>> Get(int id)
        {
            var cliente = await context.ClientesCDK.FirstOrDefaultAsync(x => x.Id == id);

            if (cliente is null)
            {
                return NotFound();
            }

            return mapper.Map<ClienteDTOs>(cliente);
        }

        [HttpPost(Name ="crearCliente")]
        public async Task<ActionResult> Post(ClienteCreacionDTOs clienteCreacionDTOs)
        {
            var ExisteVendedor = await context.VendedorCDK.AnyAsync(x => x.nombre == clienteCreacionDTOs.nombre & x.apellido == clienteCreacionDTOs.apellido);

            if (ExisteVendedor)
            {
                return BadRequest($"Ya existe un cliente de nombre {clienteCreacionDTOs.nombre} {clienteCreacionDTOs.apellido}");
            }

            var ExisteCedula = await context.ClientesCDK.AnyAsync(x => x.cedula == clienteCreacionDTOs.cedula);

            if (ExisteCedula)
            {
                return BadRequest($"Ya existe un cliente con esta cedula");
            }

            var Cliente = mapper.Map<ClienteCDK>(clienteCreacionDTOs);
            context.Add(Cliente);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}",Name ="actualizarCliente")]
        public async Task<ActionResult> Put(int id, ClienteCreacionDTOs clienteCreacionDTOs)
        {
            var ExisteCliente = await context.ClientesCDK.AnyAsync(x => x.Id == id);

            if (!ExisteCliente)
            {
                return BadRequest($"No existe cliente de Id {id}");
            }

            var clienteMap = mapper.Map<ClienteCDK>(clienteCreacionDTOs);
            clienteMap.Id = id;

            context.Update(clienteMap);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}", Name ="borrarCliente")]
        public async Task<ActionResult> Delete(int id)
        {
            var ExisteCliente = await context.ClientesCDK.AnyAsync(x => x.Id == id);

            if (!ExisteCliente)
            {
                return BadRequest($"No existe cliente de Id {id}");
            }

            context.Remove(new ClienteCDK { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutosCDK.DTOs;
using WebApiAutosCDK.Entidades;

namespace WebApiAutosCDK.Controllers
{
    [ApiController]
    [Route("api/vendedor")]
    public class VendedorController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public VendedorController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<List<ActionResult<VendedorDTOs>>> Get()
        {
            var Vendedores = await context.VendedorCDK.ToListAsync();
            return  mapper.Map<List<ActionResult<VendedorDTOs>>>(Vendedores);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<VendedorDTOs>> Get(int id)
        {
            var VendedorPorId = await context.VendedorCDK.FirstOrDefaultAsync(x => x.Id == id);

            if(VendedorPorId == null)
            {
                return BadRequest($"No existe un vendedor de Id {id}");
            }

            return mapper.Map<VendedorDTOs>(VendedorPorId);
        }

        [HttpPost]
        public async Task<ActionResult> Post(VendedorCreacionDTOs vendedorCreacionDTOs)
        {
            var ExisteVendedor = await context.VendedorCDK.AnyAsync(x => x.nombre == vendedorCreacionDTOs.nombre & x.apellido == vendedorCreacionDTOs.apellido);

            if (ExisteVendedor)
            {
                BadRequest($"Ya existe un vendedor de nombre {vendedorCreacionDTOs.nombre} y  de apellido {vendedorCreacionDTOs.apellido}");
            }

            var VendedorMapeado = mapper.Map<VendedorCDK>(ExisteVendedor);
            context.Add(VendedorMapeado);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult>Put(int id, VendedorCreacionDTOs vendedorCreacionDTOs)
        {
            var existeVendedor = await context.VendedorCDK.FirstOrDefaultAsync(x => x.Id == id);

            if (existeVendedor == null)
            {
                return BadRequest($"No existe un vendedor de id {id}");
            }

            var vendedorMapeado = mapper.Map<VendedorCDK>(vendedorCreacionDTOs);
            vendedorMapeado.Id = id;
            context.Update(vendedorMapeado);
            await context.SaveChangesAsync();
            return NoContent();

        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existeVendedor = await context.VendedorCDK.AnyAsync(x => x.Id == id);

            if (!existeVendedor)
            {
                return BadRequest($"No existe un vendedor de id {id}");
            }
     
            context.Update(new VendedorCDK {Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

       
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using autos_api_rest.Data;
using autos_api_rest.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace autos_api_rest.Controllers
{
    [Route("autos-api-rest/v1")]
    [ApiController]
    public class MarcasAutosController : ControllerBase
    {
        private readonly PostgreSqlDbContext _context;

        public MarcasAutosController(PostgreSqlDbContext context)
        {
            _context = context;
        }

        // GET: autos-api-rest/v1/listar-marcas-autos
        [HttpGet("listar-marcas-autos")]
        public async Task<ActionResult<IEnumerable<MarcaAuto>>> GetMarcasAutos()
        {
            var marcas = await _context.MarcasAutos.ToListAsync();
            return Ok(marcas);
        }
    }
}

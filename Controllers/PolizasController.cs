using Microsoft.AspNetCore.Mvc;
using ApiSeguros.Models;

namespace ApiSeguros.Controllers
{
    using Microsoft.AspNetCore.Authorization;

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PolizasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PolizasController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/polizas/registro
        [HttpPost("registro")]
        public IActionResult Registrar([FromBody] Poliza poliza)
        {
            if (_context.Polizas.Any(p => p.NumeroPoliza == poliza.NumeroPoliza))
            {
                return BadRequest("El número de póliza ya existe.");
            }
            _context.Polizas.Add(poliza);
            _context.SaveChanges();
            return Ok("Póliza registrada correctamente.");
        }

        // GET: api/polizas
        [HttpGet]
        public IActionResult Listar()
        {
            var polizas = _context.Polizas.ToList();
            return Ok(polizas);
        }

        // PUT: api/polizas/{id}
        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody] Poliza poliza)
        {
            var polizaExistente = _context.Polizas.Find(id);
            if (polizaExistente == null)
            {
                return NotFound("Póliza no encontrada.");
            }
            polizaExistente.NumeroPoliza = poliza.NumeroPoliza;
            polizaExistente.Tipo = poliza.Tipo;
            polizaExistente.FechaInicio = poliza.FechaInicio;
            polizaExistente.FechaFin = poliza.FechaFin;
            polizaExistente.MontoAsegurado = poliza.MontoAsegurado;
            _context.SaveChanges();
            return Ok("Póliza actualizada correctamente.");
        }

        // DELETE: api/polizas/{id}
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult Eliminar(int id)
        {
            var poliza = _context.Polizas.Find(id);
            if (poliza == null)
            {
                return NotFound("Póliza no encontrada.");
            }
            _context.Polizas.Remove(poliza);
            _context.SaveChanges();
            return Ok("Póliza eliminada correctamente.");
        }
    }
}

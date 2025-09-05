using Microsoft.AspNetCore.Mvc;
using ApiSeguros.Models;
using Microsoft.AspNetCore.Authorization;

namespace ApiSeguros.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/clientes/registro
        [HttpPost("registro")]
        public IActionResult Registrar([FromBody] Cliente cliente)
        {
            if (_context.Clientes.Any(c => c.Nombre == cliente.Nombre && c.Apellido == cliente.Apellido))
            {
                return BadRequest("El cliente ya existe.");
            }
            _context.Clientes.Add(cliente);
            _context.SaveChanges();
            return Ok("Cliente registrado correctamente.");
        }

        // GET: api/clientes
        [HttpGet]
        public IActionResult Listar()
        {
            var clientes = _context.Clientes.ToList();
            return Ok(clientes);
        }

        // PUT: api/clientes/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Editar(int id, [FromBody] Cliente cliente)
        {
            var clienteExistente = _context.Clientes.Find(id);
            if (clienteExistente == null)
            {
                return NotFound("Cliente no encontrado.");
            }
            clienteExistente.Nombre = cliente.Nombre;
            clienteExistente.Apellido = cliente.Apellido;
            clienteExistente.Email = cliente.Email;
            clienteExistente.Telefono = cliente.Telefono;
            _context.SaveChanges();
            return Ok("Cliente actualizado correctamente.");
        }

        // DELETE: api/clientes/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Eliminar(int id)
        {
            var cliente = _context.Clientes.Find(id);
            if (cliente == null)
            {
                return NotFound("Cliente no encontrado.");
            }
            _context.Clientes.Remove(cliente);
            _context.SaveChanges();
            return Ok("Cliente eliminado correctamente.");
        }
    }
}

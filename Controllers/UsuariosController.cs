using Microsoft.AspNetCore.Mvc;
using ApiSeguros.Models;

namespace ApiSeguros.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/usuarios/registro
        [HttpPost("registro")]
        public IActionResult Registrar([FromBody] Usuario usuario)
        {
            if (_context.Usuarios.Any(u => u.NombreUsuario == usuario.NombreUsuario))
            {
                return BadRequest("El nombre de usuario ya existe.");
            }
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return Ok("Usuario registrado correctamente.");
        }

        // POST: api/usuarios/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] Usuario usuario)
        {
            var user = _context.Usuarios.FirstOrDefault(u => u.NombreUsuario == usuario.NombreUsuario && u.Password == usuario.Password);
            if (user == null)
            {
                return Unauthorized("Usuario o contraseña incorrectos.");
            }
            return Ok("Login exitoso.");
        }
    }
}

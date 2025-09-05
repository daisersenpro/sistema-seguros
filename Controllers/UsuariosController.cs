using Microsoft.AspNetCore.Mvc;
using ApiSeguros.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiSeguros.Controllers
{
    using Microsoft.AspNetCore.Authorization;

    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        // DELETE: api/usuarios/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Eliminar(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null)
            {
                return NotFound("Usuario no encontrado.");
            }
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
            return Ok("Usuario eliminado correctamente.");
        }
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/usuarios/registro
        [HttpPost("registro")]
    [AllowAnonymous]
    public IActionResult Registrar([FromBody] Usuario usuario)
    {
        if (string.IsNullOrWhiteSpace(usuario.NombreUsuario) || string.IsNullOrWhiteSpace(usuario.Password))
        {
            return BadRequest("Nombre de usuario y contraseña son obligatorios.");
        }
        if (_context.Usuarios.Any(u => u.NombreUsuario == usuario.NombreUsuario))
        {
            return BadRequest("El nombre de usuario ya existe.");
        }
        if (usuario.TipoUsuarioId == 0 || !_context.TiposUsuario.Any(t => t.Id == usuario.TipoUsuarioId))
        {
            return BadRequest("Tipo de usuario inválido.");
        }
        try
        {
            usuario.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return Ok("Usuario registrado correctamente.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al registrar usuario: {ex.Message}");
        }
    }

        // POST: api/usuarios/login
        [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login([FromBody] Usuario usuario)
        {
            var user = _context.Usuarios.FirstOrDefault(u => u.NombreUsuario == usuario.NombreUsuario);
            if (user == null)
            {
                return Unauthorized("Usuario o contraseña incorrectos.");
            }
            // Verificar el hash de la contraseña
            bool passwordValida = BCrypt.Net.BCrypt.Verify(usuario.Password, user.Password);
            if (!passwordValida)
            {
                return Unauthorized("Usuario o contraseña incorrectos.");
            }

            // Generar el token JWT
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.NombreUsuario),
                new Claim("usuarioId", user.Id.ToString()),
                new Claim("rol", user.TipoUsuario?.Nombre ?? "")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SuperSecretKeySeguros2025"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { token = tokenString });
        }
    }
}

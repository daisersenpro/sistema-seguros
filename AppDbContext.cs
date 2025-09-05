using Microsoft.EntityFrameworkCore;
using ApiSeguros.Models;

namespace ApiSeguros
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Poliza> Polizas { get; set; }
    public DbSet<TipoUsuario> TiposUsuario { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ApiSeguros.Models
{
    public class TipoUsuario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; } // Admin, Vendedor, Cliente
        public ICollection<Usuario> Usuarios { get; set; }
    }
}

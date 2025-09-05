using System.ComponentModel.DataAnnotations;

namespace ApiSeguros.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string NombreUsuario { get; set; }
        [Required]
        public string Password { get; set; }
        public string? Email { get; set; }
    }
}

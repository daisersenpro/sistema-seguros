using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ApiSeguros.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }

        // Vendedor que creó el cliente
        public int VendedorId { get; set; }
        public Usuario Vendedor { get; set; }

        public ICollection<Poliza> Polizas { get; set; }
    }
}

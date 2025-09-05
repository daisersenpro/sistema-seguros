using System.ComponentModel.DataAnnotations;

namespace ApiSeguros.Models
{
    public class Poliza
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string NumeroPoliza { get; set; }
        [Required]
        public string Tipo { get; set; }
        public DateTimeOffset FechaInicio { get; set; }
        public DateTimeOffset FechaFin { get; set; }
        public decimal MontoAsegurado { get; set; }

        // Relación con Cliente
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        // Vendedor que vendió la póliza
        public int VendedorId { get; set; }
        public Usuario Vendedor { get; set; }
    
        // Relación con Usuario
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}

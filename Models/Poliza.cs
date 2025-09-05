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
    }
}

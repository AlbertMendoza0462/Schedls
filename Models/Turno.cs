using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schedls.Models
{
    [Table("Turnos")]
    public class Turno
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TurnoId { get; set; }
        [Required, ForeignKey(nameof(TipoTurno))]
        public int TipoTurnoId { get; set; }
        [Required]
        public DateTime FechaInicio { get; set; }
        [Required]
        public DateTime FechaFin { get; set; }

        public TipoTurno TipoTurno { get; set; }
    }
}

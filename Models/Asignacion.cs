using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schedls.Models
{
    [Table("Asignaciones")]
    public class Asignacion
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AsignacionId { get; set; }
        [Required, ForeignKey(nameof(Empleado))]
        public int EmpleadoId { get; set; }
        [Required, ForeignKey(nameof(Turno))]
        public int TurnoId { get; set; }
        [Required]
        public DateTime FechaAsignado { get; set; }

        public Empleado Empleado { get; set; }
        public Turno Turno { get; set; }
    }
}

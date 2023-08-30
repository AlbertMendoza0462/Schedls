using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schedls.Models
{
    [Table("SolcitudesCambios")]
    public class SolicitudCambio
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SolicitudCambioId { get; set; }
        [Required, ForeignKey(nameof(Empleado))]
        public int EmpleadoId { get; set; }
        [Required, ForeignKey(nameof(TurnoActual))]
        public int TurnoActualId { get; set; }
        [Required, ForeignKey(nameof(TurnoSolicitado))]
        public int TurnoSolicitadoId { get; set; }
        [Required]
        public DateTime FechaSolicitud { get; set; }
        [Required]
        public string Motivo { get; set; }
        [Required, ForeignKey(nameof(EstadoSolicitud))]
        public int EstadoSolicitudId { get; set; }
        public string? Comentario { get; set; }

        public Empleado Empleado { get; set; }
        public Turno TurnoActual { get; set; }
        public Turno TurnoSolicitado { get; set; }
        public EstadoSolicitud EstadoSolicitud { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schedls.Models
{
    [Table("Turnos")]
    public class Turno
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TurnoId { get; set; }
        [Required, ForeignKey(nameof(Usuario))]
        public int UsuarioId { get; set; }
        [Required, ForeignKey(nameof(TipoTurno))]
        public int TipoTurnoId { get; set; }
        [Required]
        public DateTime FechaInicio { get; set; }
        [Required]
        public string CantHorasEnDiaDeSemana { get; set; }
        [Required]
        public string CantHorasEnFinDeSemana { get; set; }
        [Required]
        public int IntervaloDeDias { get; set; }

        public Usuario? Usuario { get; set; }
        public TipoTurno? TipoTurno { get; set; }

        public static explicit operator Turno(SolicitudCambio solicitudCambio)
        {
            return new Turno
            {
                TurnoId = solicitudCambio.TurnoSolicitadoId,
                UsuarioId = solicitudCambio.UsuarioId,
                TipoTurnoId = solicitudCambio.TurnoSolicitado.TipoTurnoId,
                FechaInicio = solicitudCambio.FechaTurnoSolicitado,
                CantHorasEnDiaDeSemana = solicitudCambio.TurnoSolicitado.CantHorasEnDiaDeSemana,
                CantHorasEnFinDeSemana = solicitudCambio.TurnoSolicitado.CantHorasEnFinDeSemana,
                IntervaloDeDias = solicitudCambio.TurnoSolicitado.IntervaloDeDias,
                Usuario = solicitudCambio.Usuario,
                TipoTurno = solicitudCambio.TurnoSolicitado.TipoTurno
            };
        }
    }
}

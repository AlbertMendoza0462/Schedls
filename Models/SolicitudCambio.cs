using Microsoft.AspNetCore.Components;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Schedls.Models.EventoRecurrente;

namespace Schedls.Models
{
    [Table("SolicitudesCambios")]
    public class SolicitudCambio
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SolicitudCambioId { get; set; }
        [Required, ForeignKey(nameof(Usuario))]
        public int UsuarioId { get; set; }
        [Required, ForeignKey(nameof(TurnoActual))]
        public int TurnoActualId { get; set; }
        [Required, ForeignKey(nameof(TurnoSolicitado))]
        public int TurnoSolicitadoId { get; set; }
        [Required]
        public DateTime FechaTurnoActual { get; set; }
        [Required]
        public DateTime FechaTurnoSolicitado { get; set; }
        [Required]
        public DateTime FechaSolicitud { get; set; }
        [Required]
        public string Motivo { get; set; }
        [Required, ForeignKey(nameof(EstadoSolicitud))]
        public int EstadoSolicitudId { get; set; }
        public string? Comentario { get; set; }

        public Usuario? Usuario { get; set; }
        public Turno? TurnoActual { get; set; }
        public Turno? TurnoSolicitado { get; set; }
        public EstadoSolicitud? EstadoSolicitud { get; set; }

        private static string getRandColor()
        {
            string[] colores = {
                "DarkKhaki",
                "DarkSeaGreen",
                "DarkTurquoise",
                "MediumAquaMarine",
                "MediumTurquoise",
                "PaleVioletRed ",
                "Plum",
                "Brown",
                "DarkBlue",
                "DarkCyan",
                "DarkGreen",
                "ForestGreen",
                "FireBrick",
                "Fuchsia",
                "Gold",
                "Indigo",
                "LightSeaGreen",
                "MediumBlue",
                "MediumOrchid",
                "MediumPurple",
                "MediumSeaGreen",
                "Orange",
                "OrangeRed",
                "Red",
                "LimeGreen",
                "Maroon",
                "BlueViolet"
            };
            Random rnd = new Random();
            return colores[rnd.Next(colores.Length)];
            
            //Random rnd = new Random();
            //string hexOutput = String.Format("{0:X}", rnd.Next(0, 0xFFFFFF));
            //while (hexOutput.Length < 6)
            //    hexOutput = "0" + hexOutput;
            //return "#" + hexOutput;
        }

        public static List<EventoRecurrente> toEventList(List<SolicitudCambio> solicitudes)
        {
            var list = new List<EventoRecurrente>();
            solicitudes.ForEach(solicitud =>
            {
                var color = getRandColor();

                list.Add(new EventoRecurrente
                {
                    Id = solicitud.TurnoActual.TurnoId,
                    Title = solicitud.TurnoActual.TipoTurno.Abreviatura + ": " + solicitud.TurnoSolicitado.Usuario.Nombre + " " + solicitud.TurnoSolicitado.Usuario.Apellido,
                    CantHorasEnDiaDeSemana = solicitud.TurnoActual.CantHorasEnDiaDeSemana,
                    CantHorasEnFinDeSemana = solicitud.TurnoActual.CantHorasEnFinDeSemana,
                    rrule = new Rrule
                    {
                        Interval = solicitud.TurnoActual.IntervaloDeDias,
                        Dtstart = solicitud.FechaTurnoActual,
                        Until = solicitud.FechaTurnoActual.AddDays(1)
                    },
                    BackgroundColor = color
                });
                list.Add(new EventoRecurrente
                {
                    Id = solicitud.TurnoSolicitado.TurnoId,
                    Title = solicitud.TurnoSolicitado.TipoTurno.Abreviatura + ": " + solicitud.TurnoActual.Usuario.Nombre + " " + solicitud.TurnoActual.Usuario.Apellido,
                    CantHorasEnDiaDeSemana = solicitud.TurnoSolicitado.CantHorasEnDiaDeSemana,
                    CantHorasEnFinDeSemana = solicitud.TurnoSolicitado.CantHorasEnFinDeSemana,
                    rrule = new Rrule
                    {
                        Interval = solicitud.TurnoSolicitado.IntervaloDeDias,
                        Dtstart = solicitud.FechaTurnoSolicitado,
                        Until = solicitud.FechaTurnoSolicitado.AddDays(1)
                    },
                    BackgroundColor = color
                });
            });
            return list;
        }
    }
}

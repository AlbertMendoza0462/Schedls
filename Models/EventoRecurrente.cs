using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schedls.Models
{
    [NotMapped]
    public class EventoRecurrente
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CantHorasEnDiaDeSemana { get; set; }
        public string CantHorasEnFinDeSemana { get; set; }
        public Rrule rrule { get; set; }
        public List<DateTime> Exdate { get; set; } = new List<DateTime>();
        public string? BackgroundColor { get; set; }
        public string Duration { get; set; } = "01:00:00";

        public class Rrule
        {
            public string Freq { get; set; } = "daily";
            public int Interval { get; set; }
            public DateTime Dtstart { get; set; }
            public DateTime? Until { get; set; }
        }

        public static explicit operator EventoRecurrente(Turno turno)
        {
            return new EventoRecurrente
            {
                Id = turno.TurnoId,
                Title = turno.TipoTurno.Abreviatura + ": " + turno.Usuario.Nombre + " " + turno.Usuario.Apellido,
                CantHorasEnDiaDeSemana = turno.CantHorasEnDiaDeSemana,
                CantHorasEnFinDeSemana = turno.CantHorasEnFinDeSemana,
                rrule = new Rrule
                {
                    Interval = turno.IntervaloDeDias,
                    Dtstart = turno.FechaInicio,
                    Until = null,
                },
                Exdate = new List<DateTime>()
            };
        }
    }
}

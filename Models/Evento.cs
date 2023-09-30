using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schedls.Models
{
    [NotMapped]
    public class Evento
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public string CantHorasEnDiaDeSemana { get; set; }
        public string CantHorasEnFinDeSemana { get; set; }


        public static explicit operator Evento(Turno turno)
        {
            return new Evento
            {
                Id = turno.TurnoId,
                Title = turno.TipoTurno.Abreviatura + ": " + turno.Usuario.Nombre + " " + turno.Usuario.Apellido,
                Start = turno.FechaInicio,
                CantHorasEnDiaDeSemana = turno.CantHorasEnDiaDeSemana,
                CantHorasEnFinDeSemana = turno.CantHorasEnFinDeSemana,
            };
        }
    }
}
